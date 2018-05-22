#if UNITY_EDITOR || WINFORM_HOST

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

public class AppInfo {

    /// <summary>
    /// For lisibility only
    /// </summary>
    private static readonly char Dir = Path.AltDirectorySeparatorChar;

    private string _appName;

    public AppInfo(string appName, string[] scenes)
    {
        _appName = appName;
        Scenes = scenes;
    }

    private string GetSubFolder()
    {
        return Path.GetFileNameWithoutExtension(GetAppName());
    }

    public string GetAppName()
    {
        return _appName;
    }

    public string GetAppExecutableRelativePath()
    {
        return GetSubFolder() + Dir + GetAppExecutableName();
    }

    /// <summary>
    /// Get the absolute app executable path from current executing assembly
    /// NOTE: Should be used from an external app (not Unity Editor)
    /// </summary>
    /// <returns></returns>
    public string GetAppExecutableAbsolutePath()
    {
        return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), GetAppExecutableRelativePath());
    }

    public string GetAppExecutableName()
    {
        return GetAppName() + ".exe";
    }

    public readonly string[] Scenes;

    public string[] GetScenesPath()
    {
        string[] scenes = new string[Scenes.Length];
        for (int i = 0; i < Scenes.Length; i++)
        {
            scenes[i] = @"Assets" + Dir + "Scenes" + Dir + Scenes[i] + ".unity";
        }

        return scenes;
    }

#if UNITY_EDITOR

#region Build related

    private static BuildPlayerOptions GetDefaultBuildPlayerOptions()
    {
        var playerOptions = new BuildPlayerOptions()
        {
            target = BuildTarget.StandaloneWindows,
            targetGroup = BuildTargetGroup.Standalone,
        };

        return playerOptions;
    }

    private string GetOutputFolder()
    {
        string path = "Application.dataPath" + Dir + ".." + Dir + ".." + Dir + "ViveTracker.Treadmill.Configuration.Forms" + Dir + "bin";

        path += Dir + GetSubFolder();
        Directory.CreateDirectory(path);

        path += Dir + GetAppExecutableName();

        return path;
    }

    public void BuildDebug()
    {
        Build(BuildOptions.Development);
    }

    public void BuildRelease()
    {
        Build();
    }

    private void Build(BuildOptions? option = null)
    {
        Debug.Log("Build started for " + GetAppName() + "...");

        var playerOptions = GetDefaultBuildPlayerOptions();
        playerOptions.scenes = GetScenesPath();
        playerOptions.locationPathName = GetOutputFolder();
        playerOptions.options = option != null ? playerOptions.options | (BuildOptions)option : playerOptions.options;

        var result = BuildPipeline.BuildPlayer(playerOptions);

        if (string.IsNullOrEmpty(result))
        {
            Debug.Log("Build success for " + GetAppName() + "!");
        }
        else
        {
            Debug.LogError("Build failure for " + GetAppName() + "!");
        }
        
    }

#endregion

#endif
}

#endif