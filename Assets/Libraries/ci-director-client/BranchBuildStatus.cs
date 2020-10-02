namespace be.mhgamework.ci.DirectorClient
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
}