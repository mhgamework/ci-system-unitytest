using System;
using System.Text;
using ApiCopies;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace._Prototypes.CIBuildHelpers
{
    public class CIStatusWindow : EditorWindow
    {
        private const double FetchInterval = 3;
        private double nextFetch = 0;

        private BranchBuildStatus lastStatus;

        [MenuItem("CI/Open window")]
        public static void Open()
        {
            var window = (CIStatusWindow) GetWindow(typeof(CIStatusWindow), false, "CI");
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
                doGetRequest<BranchBuildStatus>("http://localhost:14001/api/branches/master/buildstatus", result =>
                {
                    if (result != null)
                    {
                        lastStatus = result;
                        Repaint();
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
                            info = JsonConvert.DeserializeObject<T>(weatherJSON);
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
            if (lastStatus == null) lastStatus = new BranchBuildStatus("Unknown");
            ConsoleWindow2.Constants.Init();
            EditorGUILayout.LabelField("Master status: ", lastStatus.Status, EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            if (lastStatus.Output != null)
                foreach (var e in lastStatus.Output.LogEntries)
                {
                    drawLine(e.Condition, e.Stacktrace);
                }

            EditorGUILayout.EndVertical();
        }

        private void drawLine(string condition, string stacktrace)
        {
            int mask = 0;
            string logString = (string) null;
            int num2 = ConsoleWindow2.Constants.LogStyleLineCount == 1 ? 4 : 8;
            int row = 1;
            GUILayout.BeginVertical((row % 2 == 0
                                        ? ConsoleWindow2.Constants.OddBackground
                                        : ConsoleWindow2.Constants.EvenBackground));
            GUILayout.BeginHorizontal(ConsoleWindow2.Constants.IconErrorStyle);
            GUILayout.BeginVertical();
            GUILayout.Label(condition, ConsoleWindow2.Constants.ErrorStyle);

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            if (!string.IsNullOrWhiteSpace(stacktrace))
            {
                var trace = StacktraceWithHyperlinks(stacktrace);
                float num = ConsoleWindow2.Constants.MessageStyle.CalcHeight(new GUIContent(trace), this.position.width);
                EditorGUILayout.SelectableLabel(trace, ConsoleWindow2.Constants.MessageStyle, GUILayout.ExpandWidth(true),
                                                GUILayout.ExpandHeight(false), GUILayout.MinHeight(num + 10f));
            }
     
            // GUIStyle styleForErrorMode1 = ConsoleWindow2.Constants.IconWarningSmallStyle;//
            //     //ConsoleWindow2.GetStyleForErrorMode(mask, true, ConsoleWindow2.Constants.LogStyleLineCount == 1);
            // Rect position1 = listViewElement.position;
            // position1.x += (float) num2;
            // position1.y += 2f;
            // styleForErrorMode1.Draw(position1, false, false, this.m_ListView.row == row, false);
            // content.text = logString;
            // GUIStyle styleForErrorMode2 =
            //     ConsoleWindow2.GetStyleForErrorMode(mask, false, ConsoleWindow2.Constants.LogStyleLineCount == 1);
            // Rect position2 = listViewElement.position;
            // position2.x += (float) num2;
            // if (string.IsNullOrEmpty(this.m_SearchText))
            // {
            //     styleForErrorMode2.Draw(position2, content, controlId, this.m_ListView.row == row);
            // }
            // else
            // {
            //     int firstSelectedCharacter = logString.IndexOf(this.m_SearchText, StringComparison.OrdinalIgnoreCase);
            //     if (firstSelectedCharacter == -1)
            //     {
            //         styleForErrorMode2.Draw(position2, content, controlId, this.m_ListView.row == row);
            //     }
            //     else
            //     {
            //         int lastSelectedCharacter = firstSelectedCharacter + this.m_SearchText.Length;
            //         Color selectionColor = GUI.skin.settings.selectionColor;
            //         styleForErrorMode2.DrawWithTextSelection(position2, content, false, true, firstSelectedCharacter,
            //                                                  lastSelectedCharacter, false, selectionColor);
            //     }
            // }
        }

        // internal static GUIStyle GetStyleForErrorMode(int mode, bool isIcon, bool isSmall)
        // {
        //     return
        //         ConsoleWindow2.HasMode(
        //             mode,
        //             ConsoleWindow2.Mode.Error | ConsoleWindow2.Mode.Assert | ConsoleWindow2.Mode.Fatal |
        //             ConsoleWindow2.Mode.AssetImportError | ConsoleWindow2.Mode.ScriptingError |
        //             ConsoleWindow2.Mode.ScriptCompileError | ConsoleWindow2.Mode.ScriptingException |
        //             ConsoleWindow2.Mode.GraphCompileError | ConsoleWindow2.Mode.ScriptingAssertion)
        //             ? (isIcon
        //                 ? (isSmall
        //                     ? ConsoleWindow2.Constants.IconErrorSmallStyle
        //                     : ConsoleWindow2.Constants.IconErrorStyle)
        //                 : (isSmall ? ConsoleWindow2.Constants.ErrorSmallStyle : ConsoleWindow2.Constants.ErrorStyle))
        //             : (ConsoleWindow2.HasMode(
        //                 mode,
        //                 ConsoleWindow2.Mode.AssetImportWarning | ConsoleWindow2.Mode.ScriptingWarning |
        //                 ConsoleWindow2.Mode.ScriptCompileWarning)
        //                 ? (isIcon
        //                     ? (isSmall
        //                         ? ConsoleWindow2.Constants.IconWarningSmallStyle
        //                         : ConsoleWindow2.Constants.IconWarningStyle)
        //                     : (isSmall
        //                         ? ConsoleWindow2.Constants.WarningSmallStyle
        //                         : ConsoleWindow2.Constants.WarningStyle))
        //                 : (isIcon
        //                     ? (isSmall
        //                         ? ConsoleWindow2.Constants.IconLogSmallStyle
        //                         : ConsoleWindow2.Constants.IconLogStyle)
        //                     : (isSmall ? ConsoleWindow2.Constants.LogSmallStyle : ConsoleWindow2.Constants.LogStyle)));
        // }

        internal static string StacktraceWithHyperlinks(string stacktraceText)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] strArray = stacktraceText.Split(new string[1]
            {
                "\n"
            }, StringSplitOptions.None);
            for (int index1 = 0; index1 < strArray.Length; ++index1)
            {
                string str1 = ") (at ";
                int num1 = strArray[index1].IndexOf(str1, StringComparison.Ordinal);
                if (num1 > 0)
                {
                    int index2 = num1 + str1.Length;
                    if (strArray[index1][index2] != '<')
                    {
                        string str2 = strArray[index1].Substring(index2);
                        int length = str2.LastIndexOf(":", StringComparison.Ordinal);
                        if (length > 0)
                        {
                            int num2 = str2.LastIndexOf(")", StringComparison.Ordinal);
                            if (num2 > 0)
                            {
                                string str3 = str2.Substring(length + 1, num2 - (length + 1));
                                string str4 = str2.Substring(0, length);
                                stringBuilder.Append(strArray[index1].Substring(0, index2));
                                stringBuilder.Append("<a href=\"" + str4 + "\" line=\"" + str3 + "\">");
                                stringBuilder.Append(str4 + ":" + str3);
                                stringBuilder.Append("</a>)\n");
                                continue;
                            }
                        }
                    }
                }

                stringBuilder.Append(strArray[index1] + "\n");
            }

            if (stringBuilder.Length > 0)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}