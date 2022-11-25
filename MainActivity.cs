global using GL = VRGeomCS.GLESBindings;

using Android.Content.PM;
using Com.Htc.VR.Sdk;
using System.Runtime.InteropServices;
using CC = Android.Content.PM.ConfigChanges;

namespace VRGeomCS;

[Activity(
    Label = "@string/app_name", 
    MainLauncher = true, LaunchMode = LaunchMode.SingleTask, 
    ScreenOrientation = ScreenOrientation.Landscape,
    ConfigurationChanges = CC.Density | CC.FontScale | CC.Keyboard | CC.KeyboardHidden | CC.LayoutDirection | 
                           CC.Locale | CC.Mnc | CC.Mcc | CC.Navigation | CC.Orientation | CC.ScreenLayout | 
                           CC.ScreenSize | CC.SmallestScreenSize | CC.UiMode | CC.Touchscreen)]
public class MainActivity : VRActivity 
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        VRInitializer.Init();
        base.OnCreate(savedInstanceState);
    }
}

public static class GLESBindings
{
    private const string Lib = "libGLESv2.so";

    [DllImport(Lib, EntryPoint = "glClearColor")]
    public static extern void ClearColor(float r, float g, float b, float a);

    [DllImport(Lib, EntryPoint = "glClear")]
    public static extern void Clear(BufferType buffer);

    [DllImport(Lib, EntryPoint = "glViewport")]
    public static extern void Viewport(int x, int y, uint width, uint height);

    [DllImport(Lib, EntryPoint = "glBindFramebuffer")]
    public static extern void GlBindFramebuffer(FramebufferTarget target, uint framebuffer);

    [DllImport(Lib, EntryPoint = "glInvalidateFramebuffer")]
    public static extern void InvalidateFramebuffer(FramebufferTarget target, uint len, ref FramebufferAttachment bufferStart);

    [DllImport(Lib, EntryPoint = "glGenFramebuffers")]
    public static extern void GenFramebuffers(uint count, ref uint bufferStart);

    [DllImport(Lib, EntryPoint = "glDeleteRenderbuffers")]
    public static extern void DeleteRenderbuffers(uint count, ref uint bufferStart);

    [DllImport(Lib, EntryPoint = "glGenRenderbuffers")]
    public static extern void GenRenderbuffers(uint count, ref uint bufferStart);

    [DllImport(Lib, EntryPoint = "glBindRenderbuffer")]
    public static extern void BindRenderbuffer(RenderbufferTarget target, uint renderbuffer);

    [DllImport(Lib, EntryPoint = "glRenderbufferStorage")]
    public static extern void RenderbufferStorage(RenderbufferTarget target, SizedInternalFormat internalFormat, uint width, uint height);

    [DllImport(Lib, EntryPoint = "glFramebufferRenderbuffer")]
    public static extern void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, RenderbufferTarget renderbuffertarget, uint renderbuffer);

    [DllImport(Lib, EntryPoint = "glFramebufferTexture2D")]
    public static extern void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget textarget, uint texture, int level);

    [DllImport(Lib, EntryPoint = "glDeleteFramebuffers")]
    public static extern void DeleteFramebuffers(uint count, ref uint bufferStart);

    public enum BufferType : int
    {
        DepthBufferBit = 0x100,
        StencilBufferBit = 0x400,
        ColorBufferBit = 0x4000
    }

    public enum FramebufferTarget : int
    {
        ReadFramebuffer = 0x8CA8,
        DrawFramebuffer = 0x8CA9,
        Framebuffer = 0x8D40
    }

    public enum FramebufferAttachment : int
    {
        DepthStencilAttachment = 0x821A,
        ColorAttachment0 = 0x8CE0,
        ColorAttachment1 = 0x8CE1,
        ColorAttachment2 = 0x8CE2,
        ColorAttachment3 = 0x8CE3,
        ColorAttachment4 = 0x8CE4,
        ColorAttachment5 = 0x8CE5,
        ColorAttachment6 = 0x8CE6,
        ColorAttachment7 = 0x8CE7,
        ColorAttachment8 = 0x8CE8,
        ColorAttachment9 = 0x8CE9,
        ColorAttachment10 = 0x8CEA,
        ColorAttachment11 = 0x8CEB,
        ColorAttachment12 = 0x8CEC,
        ColorAttachment13 = 0x8CED,
        ColorAttachment14 = 0x8CEE,
        ColorAttachment15 = 0x8CEF,
        ColorAttachment16 = 0x8CF0,
        ColorAttachment17 = 0x8CF1,
        ColorAttachment18 = 0x8CF2,
        ColorAttachment19 = 0x8CF3,
        ColorAttachment20 = 0x8CF4,
        ColorAttachment21 = 0x8CF5,
        ColorAttachment22 = 0x8CF6,
        ColorAttachment23 = 0x8CF7,
        ColorAttachment24 = 0x8CF8,
        ColorAttachment25 = 0x8CF9,
        ColorAttachment26 = 0x8CFA,
        ColorAttachment27 = 0x8CFB,
        ColorAttachment28 = 0x8CFC,
        ColorAttachment29 = 0x8CFD,
        ColorAttachment30 = 0x8CFE,
        ColorAttachment31 = 0x8CFF,
        DepthAttachment = 0x8D00,
        StencilAttachment = 0x8D20,
        ShadingRateAttachmentExt = 0x96D1
    }

    public enum SizedInternalFormat : int
    {
        DepthComponent24 = 0x81A6
    }

    public enum RenderbufferTarget : int
    {
        Renderbuffer = 0x8D41
    }

    public enum TextureTarget : int
    {
        Texture1D = 0xDE0,
        Texture2D = 0xDE1,
        ProxyTexture1D = 0x8063,
        ProxyTexture1DExt = 0x8063,
        ProxyTexture2D = 0x8064,
        ProxyTexture2DExt = 0x8064,
        Texture3D = 0x806F,
        Texture3DExt = 0x806F,
        Texture3DOes = 0x806F,
        ProxyTexture3D = 0x8070,
        ProxyTexture3DExt = 0x8070,
        DetailTexture2DSgis = 0x8095,
        Texture4DSgis = 0x8134,
        ProxyTexture4DSgis = 0x8135,
        TextureRectangle = 0x84F5,
        TextureRectangleArb = 0x84F5,
        TextureRectangleNV = 0x84F5,
        ProxyTextureRectangle = 0x84F7,
        ProxyTextureRectangleArb = 0x84F7,
        ProxyTextureRectangleNV = 0x84F7,
        TextureCubeMap = 0x8513,
        TextureCubeMapArb = 0x8513,
        TextureCubeMapExt = 0x8513,
        TextureCubeMapOes = 0x8513,
        TextureCubeMapPositiveX = 0x8515,
        TextureCubeMapPositiveXArb = 0x8515,
        TextureCubeMapPositiveXExt = 0x8515,
        TextureCubeMapPositiveXOes = 0x8515,
        TextureCubeMapNegativeX = 0x8516,
        TextureCubeMapNegativeXArb = 0x8516,
        TextureCubeMapNegativeXExt = 0x8516,
        TextureCubeMapNegativeXOes = 0x8516,
        TextureCubeMapPositiveY = 0x8517,
        TextureCubeMapPositiveYArb = 0x8517,
        TextureCubeMapPositiveYExt = 0x8517,
        TextureCubeMapPositiveYOes = 0x8517,
        TextureCubeMapNegativeY = 0x8518,
        TextureCubeMapNegativeYArb = 0x8518,
        TextureCubeMapNegativeYExt = 0x8518,
        TextureCubeMapNegativeYOes = 0x8518,
        TextureCubeMapPositiveZ = 0x8519,
        TextureCubeMapPositiveZArb = 0x8519,
        TextureCubeMapPositiveZExt = 0x8519,
        TextureCubeMapPositiveZOes = 0x8519,
        TextureCubeMapNegativeZ = 0x851A,
        TextureCubeMapNegativeZArb = 0x851A,
        TextureCubeMapNegativeZExt = 0x851A,
        TextureCubeMapNegativeZOes = 0x851A,
        ProxyTextureCubeMap = 0x851B,
        ProxyTextureCubeMapArb = 0x851B,
        ProxyTextureCubeMapExt = 0x851B,
        Texture1DArray = 0x8C18,
        ProxyTexture1DArray = 0x8C19,
        ProxyTexture1DArrayExt = 0x8C19,
        Texture2DArray = 0x8C1A,
        ProxyTexture2DArray = 0x8C1B,
        ProxyTexture2DArrayExt = 0x8C1B,
        TextureBuffer = 0x8C2A,
        Renderbuffer = 0x8D41,
        TextureCubeMapArray = 0x9009,
        TextureCubeMapArrayArb = 0x9009,
        TextureCubeMapArrayExt = 0x9009,
        TextureCubeMapArrayOes = 0x9009,
        ProxyTextureCubeMapArray = 0x900B,
        ProxyTextureCubeMapArrayArb = 0x900B,
        Texture2DMultisample = 0x9100,
        ProxyTexture2DMultisample = 0x9101,
        Texture2DMultisampleArray = 0x9102,
        ProxyTexture2DMultisampleArray = 0x9103
    }
}