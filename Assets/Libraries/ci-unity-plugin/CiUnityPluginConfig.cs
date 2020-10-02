namespace be.mhgamework.ci.UnityPlugin
{
    public class CiUnityPluginConfig
    {
        public static string ServerAddress { get; private set; }
        public static void Init(string serverAddress)
        {
            ServerAddress = serverAddress;
        }
    }
}