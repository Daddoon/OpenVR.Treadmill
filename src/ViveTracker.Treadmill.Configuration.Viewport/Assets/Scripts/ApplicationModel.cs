using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#endif

public static class ApplicationModel {

#if UNITY_EDITOR || WINFORM_HOST

    public static class Apps
    {
        public static readonly AppInfo Viewport = new AppInfo(nameof(Viewport), new string[]
        {
            AppScenes.ViewportScene
        });

        public static readonly AppInfo Gamepad = new AppInfo(nameof(Gamepad), new string[]
        {
            AppScenes.GamepadScene
        });
    }

#endif

    public static class AppScenes
    {
        public const string ViewportScene = nameof(ViewportScene);

        public const string GamepadScene = nameof(GamepadScene);
    }
}
