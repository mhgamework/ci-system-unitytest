namespace be.mhgamework.ci.UnityPlugin
{
    public class CiUnityPluginConfig
    {
        public static string ServerAddress { get; private set; }
        public static string DefaultBranch { get; set; } = "master";

        public static void Init(string serverAddress)
        {
            ServerAddress = serverAddress;
        }
    }
}