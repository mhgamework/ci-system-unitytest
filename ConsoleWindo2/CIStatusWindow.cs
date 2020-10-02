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

        [MenuItem("CI/Open window")]
        public static void Open()
        {
            var window = (CIStatusWindow)GetWindow(typeof(CIStatusWindow),false,"CI");
            window.Show();
        }
        private void OnEnable()
        {
            EditorApplication.update += onUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= onUpdate;
        }

        private void onUpdate()
        {
            var currentTime = EditorApplication.timeSinceStartup;
            if (nextFetch < currentTime)
            {
                //TODO: this can cause cascading fetches
                nextFetch = EditorApplication.timeSinceStartup + FetchInterval;

                doGetRequest<object>("http://localhost:14001/jobs", result =>
                {
                    if (result != null)
                    {
                        Debug.Log(result);
                    }
                });
            }
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
            EditorGUILayout.LabelField("Master status: ","Failed");
        }
    }
}