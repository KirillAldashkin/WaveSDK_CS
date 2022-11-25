using Android.Opengl;
using System.Runtime.CompilerServices;

namespace VRGeomCS;

public class VRApp
{
    public bool Running { get; set; } = false;

    private int width, height;
    private WVR.TextureQueueHandle leftEyeQ, rightEyeQ;
    private List<FrameBufferObject> leftFBOs = new(), rightFBOs = new();

    #region Init & Shutdown
    internal bool InitVR()
    {
        var initErr = WVR.Init();
        if (initErr != WVR.InitError.None)
        {
            Console.WriteLine($"Init error: {initErr}");
            return false;
        }

        var renderInitParams = new WVR.RenderInitParams()
        {
            GraphicsApi = WVR.GraphicsApiType.OpenGL,
            RenderConfig = WVR.RenderConfig.Default
        };
        System.Diagnostics.Debug.WriteLine(new string('\n', 60));
        var renderInitErr = WVR.RenderInit(ref renderInitParams);
        if(renderInitErr != WVR.RenderError.None)
        {
            Console.WriteLine($"RenderInit error: {renderInitErr}");
            return false;
        }

        Span<WVR.InputAttribute> array = stackalloc WVR.InputAttribute[] {
            new()
            { 
                ID = WVR.InputID.Menu, 
                Capability = WVR.InputType.Button, 
                AxisType = WVR.AnalogType.None 
            },
            new() 
            { 
                ID = WVR.InputID.Grip, 
                Capability = WVR.InputType.Button, 
                AxisType = WVR.AnalogType.None 
            },
            new()
            { 
                ID = WVR.InputID.Touchpad, 
                Capability = WVR.InputType.Button | WVR.InputType.Analog, 
                AxisType = WVR.AnalogType.D2D 
            },
            new()
            {
                ID = WVR.InputID.Trigger,
                Capability = WVR.InputType.Button | WVR.InputType.Analog,
                AxisType = WVR.AnalogType.D1D
            }
        };

        WVR.SetInputRequest(WVR.DeviceType.ControllerRight, ref array[0], array.Length);
        WVR.SetInputRequest(WVR.DeviceType.ControllerLeft, ref array[0], array.Length);

        return true;
    }

    internal bool InitGL()
    {
        GLES20.GlEnable(GLES20.GlDepthTest);
        GLES20.GlDepthFunc(GLES20.GlLequal);
        GLES20.GlDepthMask(true);

        WVR.GetRenderTargetSize(out width, out height);
        if (width == 0 || height == 0) return false;

        leftEyeQ = WVR.ObtainTextureQueue(WVR.TextureTarget.D2D, WVR.TextureFormat.RGBA, WVR.TextureType.UnsignedByte, (uint)width, (uint)height, 0);
        rightEyeQ = WVR.ObtainTextureQueue(WVR.TextureTarget.D2D, WVR.TextureFormat.RGBA, WVR.TextureType.UnsignedByte, (uint)width, (uint)height, 0);
        if (!ProcessEye(leftEyeQ, leftFBOs)) return false;
        if (!ProcessEye(rightEyeQ, rightFBOs)) return false;

        return true;

        bool ProcessEye(WVR.TextureQueueHandle eye, List<FrameBufferObject> to)
        {
            for (int i = 0; i < WVR.GetTextureQueueLength(eye); i++) {
                var fbo = FrameBufferObject.TryCreate((uint)WVR.GetTexture(eye, i).Id, (uint)width, (uint)height);
                if (fbo is null) return false;
                to.Add(fbo); 
            }
            return true;
        }
    }

    internal void ShutdownGL()
    {
        foreach (var fbo in leftFBOs) fbo.Clear();
        foreach (var fbo in rightFBOs) fbo.Clear();
        WVR.ReleaseTextureQueue(leftEyeQ);
        WVR.ReleaseTextureQueue(rightEyeQ);
    }

    internal void ShutdownVR() => WVR.Quit();
    #endregion

    internal void HandleInput()
    {

    }


    internal bool RenderFrame()
    {
        Console.WriteLine(">>>>>> RenderFrame <<<<<<");
        var indexLeft = WVR.GetAvailableTextureIndex(leftEyeQ);
        var indexRight = WVR.GetAvailableTextureIndex(rightEyeQ);

        var leftEyeTexture = WVR.GetTexture(leftEyeQ, indexLeft);
        var rightEyeTexture = WVR.GetTexture(rightEyeQ, indexRight);

        // Render left eye
        var leftEyeFBO = leftFBOs[indexLeft];
        leftEyeFBO.Bind();
        leftEyeFBO.FullViewport();
        WVR.PreRenderEye(WVR.Eye.Left, ref leftEyeTexture, ref Unsafe.NullRef<WVR.RenderFoveationParams>());
        GL.ClearColor(1, 0, 0, 1);
        GL.Clear(GL.BufferType.ColorBufferBit | GL.BufferType.DepthBufferBit);
        leftEyeFBO.Unbind();

        // Render right eye
        var rightEyeFBO = rightFBOs[indexRight];
        rightEyeFBO.Bind();
        rightEyeFBO.FullViewport();
        WVR.PreRenderEye(WVR.Eye.Right, ref rightEyeTexture, ref Unsafe.NullRef<WVR.RenderFoveationParams>());
        GL.ClearColor(0, 0, 1, 1);
        GL.Clear(GL.BufferType.ColorBufferBit | GL.BufferType.DepthBufferBit);
        rightEyeFBO.Unbind();

        // Submit left eye
        var e = WVR.SubmitFrame(WVR.Eye.Left, ref leftEyeTexture, ref Unsafe.NullRef<WVR.PoseState>(), WVR.SubmitExtend.Default);
        if (e != WVR.SubmitError.None) return false;

        // Submit right eye
        e = WVR.SubmitFrame(WVR.Eye.Right, ref rightEyeTexture, ref Unsafe.NullRef<WVR.PoseState>(), WVR.SubmitExtend.Default);
        if (e != WVR.SubmitError.None) return false;

        return true;
    }
}
