using System.Linq;
using be.mhgamework.ci.UnityPlugin;
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// @unfinished: currently named BuildScript because ci uses this as default
/// </summary>
public class BuildScript
{
    /// <summary>
    /// Performs the command line build by using the passed command line arguments.
    /// </summary>
    [MenuItem("CISample/TestBuild")]

    public static void Build()
    {
        SimpleCIBuildScript.Build(RunUnityBuild,"BuildAuto/Starship Troopers.exe");
    }

    public static BuildReport RunUnityBuild(SimpleCIBuildScript.Options opts)
    {
        var options = new BuildPlayerOptions();
        options.target = BuildTarget.StandaloneWindows64;
        options.locationPathName = opts.BuildOutputExePath;
        options.scenes = new[]
        {
            "Assets/Scenes/SampleScene.unity",
        };

        var buildMessage = BuildPipeline.BuildPlayer(options);
        return buildMessage;
    }
}

#endif