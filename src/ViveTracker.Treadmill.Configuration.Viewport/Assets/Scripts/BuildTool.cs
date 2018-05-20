using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;


public class BuildTools {

    [MenuItem("Build/Build Viewport (Debug)")]
    public static void BuildViewportDebug()
    {
        ApplicationModel.Apps.Viewport.BuildDebug();
    }

    [MenuItem("Build/Build Viewport (Release)")]
    public static void BuildViewportRelease()
    {
        ApplicationModel.Apps.Viewport.BuildRelease();
    }

    [MenuItem("Build/Build Gamepad (Debug)")]
    public static void BuildGamepadDebug()
    {
        ApplicationModel.Apps.Gamepad.BuildDebug();
    }

    [MenuItem("Build/Build Gamepad (Release)")]
    public static void BuildGamepadRelease()
    {
        ApplicationModel.Apps.Gamepad.BuildRelease();
    }
}

#endif