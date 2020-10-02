using be.mhgamework.ci.UnityPlugin;
using UnityEditor;

namespace DefaultNamespace
{
    [InitializeOnLoad]
    public class CIConfiguration
    {
        static CIConfiguration()
        {
            CiUnityPluginConfig.Init("http://localhost:14001/");
        }
    }
}