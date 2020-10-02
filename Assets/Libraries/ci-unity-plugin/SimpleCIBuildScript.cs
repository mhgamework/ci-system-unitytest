using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace be.mhgamework.ci.UnityPlugin
{
    /// <summary>
    /// Example simple CI buildscript entrypoint, compatible with the CI system
    /// </summary>
    public class SimpleCIBuildScript
    {
        public class Options
        {
            public string BuildOutputExePath;
        }

        /// <summary>
        /// Use this call as the entry point for the CI system (build script)
        /// @unfinished ci should probably use commandline args to set output locations
        /// </summary>
        /// <param name="build"></param>
        /// <param name="relativeBuildOutputExe"></param>
        public static void Build(Func<Options, BuildReport> build, string relativeBuildOutputExe)
        {
            //        string[] args = System.Environment.GetCommandLineArgs();
            var publishPath = Path.Combine(Environment.CurrentDirectory, relativeBuildOutputExe);
            BuildOutputPlugin.CaptureBuildOutput(() => runBuild(build, publishPath));
        }

        private static bool runBuild(Func<Options, BuildReport> build, string buildOutputExePath)
        {
            Debug.Log("[[[BUILD]]] Starting build");
            EditorUtility.DisplayProgressBar("Building Starship Troopers", "Executing build of unity project", 0);
            try
            {
                if (Application.isBatchMode)
                    EditorUtility.audioMasterMute = true; // Mute all during build audio!
                var buildPathFullName = new FileInfo(buildOutputExePath).Directory.FullName;

                Directory.CreateDirectory(buildPathFullName);
                var opts = new Options()
                {
                    BuildOutputExePath = buildOutputExePath
                };
                var buildMessage = build(opts);
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
                    if (Application.isBatchMode) EditorApplication.Exit(100);
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
}