﻿using System;
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

    [DllImport("libwvr_api", EntryPoint = "WVR_RenderInit")]
    public static extern RenderError RenderInit(in RenderInitParams param);

    [DllImport("libwvr_api", EntryPoint = "WVR_SetInputRequest")]
    public static extern bool SetInputRequest(DeviceType type, ref InputAttribute first, int size);

    [DllImport("libwvr_api", EntryPoint = "WVR_GetRenderTargetSize")]
    public static extern void GetRenderTargetSize(out int width, out int height);

    [DllImport("libwvr_api", EntryPoint = "WVR_ObtainTextureQueue")]
    public static extern TextureQueueHandle ObtainTextureQueue(TextureTarget target, TextureFormat format, TextureType type, uint width, uint height, int level);

    [DllImport("libwvr_api", EntryPoint = "WVR_GetTextureQueueLength")]
    public static extern uint GetTextureQueueLength(TextureQueueHandle handle);

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

    public enum GraphicsApiType
    {
        OpenGL = 1
    }

    public enum RenderConfig : ulong
    {
        Default = 0,
        DisableSingleBuffer = 1,
        DisableReprojection = 2,
        sRGB = 4,
        GLNoError = 8,
        InitializeFadeOut = 32,
        InitializeUMC = 64,
        InitializePMC = 128,
        InitializeFrameSharpnessEnhancement = 256
    }

    public enum RenderError
    {
        None = 0,
        RuntimeInitFailed = 410,
        ContextSetupFailed = 411,
        DisplaySetupFailed = 412,
        LibNotSupported = 413,
        NullPtr = 414,
        Max = 65535,
    }

    public struct RenderInitParams
    {
        public GraphicsApiType GraphicsApi;
        public RenderConfig RenderConfig;
        public GraphicsParams GraphicsParams;
    }

    public struct GraphicsParams
    {
        public IntPtr RenderContext;
    }

    public struct InputAttribute
    {
        public InputID ID;
        public InputType Capability;
        public AnalogType AxisType;
    }

    public enum InputID {
        Id0 = 0,
        Id1 = 1,
        Id2 = 2,
        Id3 = 3,
        Id4 = 4,
        Id5 = 5,
        Id6 = 6,
        Id7 = 7,
        Id8 = 8,
        Id9 = 9,
        Id10 = 10,
        Id11 = 11,
        Id12 = 12,
        Id13 = 13,
        Id14 = 14,
        Id15 = 15,
        Id16 = 16,
        Id17 = 17,
        Id18 = 18,
        Id19 = 19,

        System = Id0,
        Menu = Id1,
        Grip = Id2,
        DPadLeft = Id3,
        DPadUp = Id4,
        DPadRight = Id5,
        DPadDown = Id6,
        VolumeUp = Id7,
        VolumeDown = Id8,
        Bumper = Id9,
        A = Id10,
        B = Id11,
        X = Id12,
        Y = Id13,
        Back = Id14,
        Enter = Id15,
        Touchpad = Id16,
        Trigger = Id17,
        Thumbstick = Id18,
        Parking = Id19,

        Max = 32
    }

    public enum AnalogType {
        None = 0,
        D2D = 1,
        D1D = 2,
    }

    public enum InputType
    {
        Button = 1,
        Touch = 2,
        Analog = 4,
    }

    public enum DeviceType
    {
        Invalid = 0,
        HMD = 1,
        ControllerRight = 2,
        ControllerLeft = 3,
        Camera = 4,
        EyeTracking = 5,
        HandGestureRight = 6,
        HandGestureLeft = 7,
        NaturalHandRight = 8,
        NaturalHandLeft = 9,
        ElectronicHandRight = 10,
        ElectronicHandLeft = 11,
        Tracker = 12,
        Lip = 13,
        EyeExpression = 14
    }

    public enum TextureTarget
    {
        D2D,
        Array2D,
        External2D,
        DUAL2D,
        ExtDual2D,
        Vulkan
    }

    public enum TextureFormat
    {
        RGBA
    }

    public enum TextureType
    {
        UnsignedByte
    }

    public struct TextureQueueHandle
    {
        public IntPtr Handle;
    }
}
