using System.Collections.Generic;

namespace be.mhgamework.ci.DirectorClient
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
}