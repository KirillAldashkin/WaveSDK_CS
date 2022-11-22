using Android.Content.PM;
using Com.Htc.VR.Sdk;
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
