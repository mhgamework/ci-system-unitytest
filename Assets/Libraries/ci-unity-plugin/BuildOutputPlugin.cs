using System;
using System.Collections.Generic;
using System.IO;
using be.mhgamework.ci.DirectorClient;
using Newtonsoft.Json;
using UnityEngine;

namespace be.mhgamework.ci.UnityPlugin
{
    /// <summary>
    /// @unfinished: might be better to use unitys BuildResult class instead of doing something manually.
    /// However, this does not handle any custom steps done in the build    
    /// </summary>
    public class BuildOutputPlugin
    {
        public static BuildOutput CaptureBuildOutput(Func<bool> build)
        {
            var result = new BuildOutput();
            Application.LogCallback logCallback = (a, b, c) =>
                                                  {
                                                      if (c == LogType.Log || c == LogType.Warning) return;
                                                      result.LogEntries.Add(new BuildOutput.Entry(a, b, c.ToString()));
                                                  };
            Application.logMessageReceived += logCallback;

            try
            {
                result.IsSuccess = build();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                result.IsSuccess = false;
            }
            finally
            {
                Application.logMessageReceived -= logCallback;
                File.WriteAllText("TempBuildOutput.json", JsonConvert.SerializeObject(result));
            }
            return result;
        }
    }
}