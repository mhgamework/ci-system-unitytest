using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace DefaultNamespace._Prototypes.CIBuildHelpers
{
    /// <summary>
    /// @unfinished: might be better to use unitys BuildResult class instead of doing something manually.
    /// However, this does not handle any custom steps done in the build    
    /// </summary>
    public class BuildOutputPlugin
    {
        public class BuildOutput
        {
            public bool IsSuccess { get; set; }
            public List<Entry> LogEntries { get; set; } = new List<Entry>();

            public class Entry
            {
                public string LogType { get; set; }
                public string Condition { get; set; }
                public string Stacktrace { get; set; }

                public Entry()
                {
                }

                public Entry(string condition, string stacktrace, string logType)
                {
                    LogType = logType;
                    Condition = condition;
                    Stacktrace = stacktrace;
                }
            }
        }

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