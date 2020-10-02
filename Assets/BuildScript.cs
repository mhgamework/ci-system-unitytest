using System.Linq;
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace._Prototypes.CIBuildHelpers;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class Builder
{
    public static string DefaultOutputPath = "BuildAuto/Starship Troopers.exe";

    public static bool Build(string buildPath)
    {
        Debug.Log("[[[BUILD]]] Starting build");
        EditorUtility.DisplayProgressBar("Building Starship Troopers", "Executing build of unity project", 0);
        try
        {
            //EditorUtility.audioMasterMute = true; // Mute all during build audio!
            //File.WriteAllText("environment.txt", OSUtilities.GetAllEnvironmentVariables());
            var buildPathFullName = new FileInfo(buildPath).Directory.FullName;
            var streamingAssetsBuildPath = Path.Combine(buildPathFullName, "Starship Troopers_Data", "StreamingAssets");

            Directory.CreateDirectory(buildPathFullName);

            //BuildInfoFile.RemoveBuildInfoFile(streamingAssetsBuildPath);

            var options = new BuildPlayerOptions();
            options.target = BuildTarget.StandaloneWindows64;
            options.locationPathName = buildPath;
            options.scenes = new[]
            {
                "Assets/Scenes/SampleScene.unity",
            };

            var buildMessage = BuildPipeline.BuildPlayer(options);
            if (buildMessage.summary.result == BuildResult.Succeeded)
            {
                if (!Application.isBatchMode)
                    EditorUtility.DisplayDialog("Build Starship Troopers", "Build was successful", "Ok");
                Debug.Log("[[[BUILD]]] Build succeeded");
                return true;
            }
            else
            {
                if (!Application.isBatchMode)
                    EditorUtility.DisplayDialog("Build Starship Troopers", "Build failed with errors", "Ok");
                Debug.Log("[[[BUILD]]] Build failed");
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return false;
        }
        finally
        {
            EditorUtility.ClearProgressBar();
            Debug.Log("[[[BUILD]]] Build finalized");
        }
    }
}

public class BuildScript
{
    /// <summary>
    /// Performs the command line build by using the passed command line arguments.
    /// </summary>
    public static void Build()
    {
        //@unfinished ci should probably use commandline args to set output locations
        //        string[] args = System.Environment.GetCommandLineArgs();

        var publishPath = Path.Combine(Environment.CurrentDirectory, Builder.DefaultOutputPath);
        var result = BuildOutputPlugin.CaptureBuildOutput(() => Builder.Build(publishPath));
        if (!result.IsSuccess && Application.isBatchMode) EditorApplication.Exit(100);
    }
}

#endif