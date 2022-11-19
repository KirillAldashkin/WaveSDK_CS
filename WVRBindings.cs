using System.Runtime.InteropServices;

namespace VRGeomCS;

public static unsafe class WVR
{
    [DllImport("libwvr_api", EntryPoint = "WVR_RegisterMain")]
    public static extern void RegisterMain(delegate* unmanaged[Cdecl]<int, sbyte**, int> mainFunc);

    [DllImport("libwvr_api", EntryPoint = "WVR_Init")]
    public static extern InitError Init(AppType type = AppType.VRContent);

    [DllImport("libwvr_api", EntryPoint = "WVR_Quit")]
    public static extern void Quit();

    public enum InitError
    {
        None = 0,
        Unknown = 1,
        NotInitialized = 2
    }

    public enum AppType
    {
        VRContent = 1,
        ARContent = 2
    }
}
