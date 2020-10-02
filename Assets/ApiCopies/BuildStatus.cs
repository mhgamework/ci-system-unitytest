using System.Collections.Generic;

namespace ApiCopies
{
    public class BranchBuildStatus
    {
        public string Status { get; set; }
        public BuildOutput Output { get; set; }

        public BranchBuildStatus()
        {
        }

        public BranchBuildStatus(string status)
        {
            Status = status;
        }
    }
    /// <summary>
    /// @unfinished: this is a plugin class and public api, need better place to put this. Defined by the unity plugin that generates this report
    /// </summary>
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
}