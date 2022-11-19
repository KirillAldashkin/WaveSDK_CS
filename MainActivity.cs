using Android.Content.PM;
using Com.Htc.VR.Sdk;
using System.Runtime.CompilerServices;
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
        unsafe
        {
            WVR.RegisterMain(&Main);
        }
        base.OnCreate(savedInstanceState);
    }

    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static unsafe int Main(int argc, sbyte** args)
    {
        var vr = new VRApp();

        if (!vr.InitVR())
        {
            vr.ShutdownVR();
            return 1;
        }

        if (!vr.InitGL())
        {
            vr.ShutdownGL();
            vr.ShutdownVR();
            return 1;
        }

        try
        {
            while (true)
            {
                vr.HandleInput();
                if (!vr.Running) break;
                vr.RenderFrame();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            vr.ShutdownGL();
            vr.ShutdownVR();
        }

        return 0;
    }
}