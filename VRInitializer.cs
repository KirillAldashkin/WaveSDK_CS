﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace VRGeomCS;

public static class VRInitializer
{
    public static unsafe void Init() => WVR.RegisterMain(&Main);


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
            vr.Running = true;
            while (true)
            {
                vr.HandleInput();
                if (!vr.Running) break;
                if (!vr.RenderFrame()) break;
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