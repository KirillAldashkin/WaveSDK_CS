using System;

namespace VRGeomCS;

public class VRApp
{
    public bool Running { get; set; } = false;

    private int width, height;
    private WVR.TextureQueueHandle leftEye, rightEye;

    #region Init & Shutdown
    internal bool InitVR()
    {
        var initErr = WVR.Init();
        if (initErr != WVR.InitError.None)
        {
            Console.WriteLine($"Init error: {initErr}");
            return false;
        }

        var renderInitErr = WVR.RenderInit(new()
        {
            GraphicsApi = WVR.GraphicsApiType.OpenGL,
            RenderConfig = WVR.RenderConfig.Default
        });
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
        WVR.GetRenderTargetSize(out width, out height);
        if (width == 0 || height == 0) return false;

        leftEye = WVR.ObtainTextureQueue(WVR.TextureTarget.D2D, WVR.TextureFormat.RGBA, WVR.TextureType.UnsignedByte, (uint)width, (uint)height, 0);
        if(!ProcessEye(leftEye)) return false;
        rightEye = WVR.ObtainTextureQueue(WVR.TextureTarget.D2D, WVR.TextureFormat.RGBA, WVR.TextureType.UnsignedByte, (uint)width, (uint)height, 0);
        if(!ProcessEye(rightEye)) return false;

        return true;

        bool ProcessEye(WVR.TextureQueueHandle eye)
        {
            for (int i = 0; i < WVR.GetTextureQueueLength(eye); i++) {
                /*
                FrameBufferObject* fbo;

                fbo = new FrameBufferObject((int)(long)WVR_GetTexture(mLeftEyeQ, i).id, mRenderWidth, mRenderHeight, true);
                if (!fbo) return false;
                if (fbo->hasError())  {
                    delete fbo;
                    return false;
                }
                mLeftEyeFBOMSAA.push_back(fbo);

                fbo = new FrameBufferObject((int)(long)WVR_GetTexture(mLeftEyeQ, i).id, mRenderWidth, mRenderHeight);
                if (!fbo) return false;
                if (fbo->hasError())  {
                    delete fbo;
                    return false;
                }
                mLeftEyeFBO.push_back(fbo); 
                */
            }
            return true;
        }
    }
    internal void ShutdownGL()
    {
        
    }

    internal void ShutdownVR() => WVR.Quit();
    #endregion

    internal void HandleInput()
    {

    }


    internal bool RenderFrame()
    {

        return true;
    }

    internal void UpdateHMDMatrixPose()
    {

    }
}
