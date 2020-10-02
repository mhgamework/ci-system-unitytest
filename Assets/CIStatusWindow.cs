using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace._Prototypes.CIBuildHelpers
{
    public class CIStatusWindow : EditorWindow
    {
        private const double FetchInterval = 3;
        private double nextFetch = 0;
        private void Awake()
        {
            EditorApplication.update += onUpdate;
        }

        private void OnDestroy()
        {
            EditorApplication.update -= onUpdate;
        }

        private void onUpdate()
        {
            var currentTime = EditorApplication.timeSinceStartup;
            if (nextFetch < currentTime)
            {
                doGetRequest<object>("https://localhost:15001/jobs", result =>
                {
                    nextFetch = EditorApplication.timeSinceStartup + FetchInterval;
                    if (result != null)
                    {
                        Debug.Log(result);
                    }
                });
            }
            throw new NotImplementedException();
        }

        private void doGetRequest<T>(string url, Action<T> onResult)
        {
            UnityWebRequest req = UnityWebRequest.Get(url);
            {
                var operation = req.SendWebRequest();
                operation.completed += op =>
                {
                    T info = default(T);

                    try
                    {
                        if (req.responseCode == 200)
                        {
                            byte[] result = req.downloadHandler.data;
                            string weatherJSON = System.Text.Encoding.Default.GetString(result);
                            info = JsonUtility.FromJson<T>(weatherJSON);
                        }
                    }
                    finally
                    {
                        req.Dispose();
                    }

                    onResult(info);
                };
            }  
        }
          

        private void OnGUI()
        {
            throw new NotImplementedException();
        }
    }
}