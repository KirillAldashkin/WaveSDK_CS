using System;

namespace VRGeomCS;

public class VRApp
{
    public bool Running { get; set; } = false;

    internal bool InitVR()
    {
        var err = WVR.Init();
        if (err != WVR.InitError.None)
        {
            Console.WriteLine($"Init VR error: {err}");
        }
        return err == WVR.InitError.None;
    }

    internal void HandleInput()
    {

    }

    internal bool InitGL()
    {
        return false;
    }

    internal void RenderFrame()
    {

    }

    internal void ShutdownGL()
    {

    }

    internal void ShutdownVR()
    {
        WVR.Quit();
    }
}