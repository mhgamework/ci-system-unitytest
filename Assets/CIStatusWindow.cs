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
            Constants.Init();
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
            int num2 = Constants.LogStyleLineCount == 1 ? 4 : 8;
            int row = 1;
            GUILayout.BeginVertical((row % 2 == 0
                                        ? Constants.OddBackground
                                        : Constants.EvenBackground));
            GUILayout.BeginHorizontal(Constants.IconErrorStyle);
            GUILayout.BeginVertical();
            GUILayout.Label(condition, Constants.ErrorStyle);

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            if (!string.IsNullOrWhiteSpace(stacktrace))
            {
                var trace = StacktraceWithHyperlinks(stacktrace);
                float num = Constants.MessageStyle.CalcHeight(
                    new GUIContent(trace), this.position.width);
                EditorGUILayout.SelectableLabel(trace, Constants.MessageStyle,
                                                GUILayout.ExpandWidth(true),
                                                GUILayout.ExpandHeight(false), GUILayout.MinHeight(num + 10f));
            }

            // GUIStyle styleForErrorMode1 = Constants.IconWarningSmallStyle;//
            //     //GetStyleForErrorMode(mask, true, Constants.LogStyleLineCount == 1);
            // Rect position1 = listViewElement.position;
            // position1.x += (float) num2;
            // position1.y += 2f;
            // styleForErrorMode1.Draw(position1, false, false, this.m_ListView.row == row, false);
            // content.text = logString;
            // GUIStyle styleForErrorMode2 =
            //     GetStyleForErrorMode(mask, false, Constants.LogStyleLineCount == 1);
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
        //         HasMode(
        //             mode,
        //             Mode.Error | Mode.Assert | Mode.Fatal |
        //             Mode.AssetImportError | Mode.ScriptingError |
        //             Mode.ScriptCompileError | Mode.ScriptingException |
        //             Mode.GraphCompileError | Mode.ScriptingAssertion)
        //             ? (isIcon
        //                 ? (isSmall
        //                     ? Constants.IconErrorSmallStyle
        //                     : Constants.IconErrorStyle)
        //                 : (isSmall ? Constants.ErrorSmallStyle : Constants.ErrorStyle))
        //             : (HasMode(
        //                 mode,
        //                 Mode.AssetImportWarning | Mode.ScriptingWarning |
        //                 Mode.ScriptCompileWarning)
        //                 ? (isIcon
        //                     ? (isSmall
        //                         ? Constants.IconWarningSmallStyle
        //                         : Constants.IconWarningStyle)
        //                     : (isSmall
        //                         ? Constants.WarningSmallStyle
        //                         : Constants.WarningStyle))
        //                 : (isIcon
        //                     ? (isSmall
        //                         ? Constants.IconLogSmallStyle
        //                         : Constants.IconLogStyle)
        //                     : (isSmall ? Constants.LogSmallStyle : Constants.LogStyle)));
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

        internal class Constants
        {
            public static readonly string ClearLabel = L10n.Tr("Clear");
            public static readonly string ClearOnPlayLabel = L10n.Tr("Clear on Play");
            public static readonly string ErrorPauseLabel = L10n.Tr("Error Pause");
            public static readonly string CollapseLabel = L10n.Tr("Collapse");
            public static readonly string StopForAssertLabel = L10n.Tr("Stop for Assert");
            public static readonly string StopForErrorLabel = L10n.Tr("Stop for Error");
            public static readonly string ClearOnBuildLabel = L10n.Tr("Clear on Build");
            private static bool ms_Loaded;
            private static int ms_logStyleLineCount;
            public static GUIStyle Box;
            public static GUIStyle MiniButtonLeft;
            public static GUIStyle MiniButton;
            public static GUIStyle MiniButtonRight;
            public static GUIStyle LogStyle;
            public static GUIStyle WarningStyle;
            public static GUIStyle ErrorStyle;
            public static GUIStyle IconLogStyle;
            public static GUIStyle IconWarningStyle;
            public static GUIStyle IconErrorStyle;
            public static GUIStyle EvenBackground;
            public static GUIStyle OddBackground;
            public static GUIStyle MessageStyle;
            public static GUIStyle StatusError;
            public static GUIStyle StatusWarn;
            public static GUIStyle StatusLog;
            public static GUIStyle Toolbar;
            public static GUIStyle CountBadge;
            public static GUIStyle LogSmallStyle;
            public static GUIStyle WarningSmallStyle;
            public static GUIStyle ErrorSmallStyle;
            public static GUIStyle IconLogSmallStyle;
            public static GUIStyle IconWarningSmallStyle;
            public static GUIStyle IconErrorSmallStyle;

            public static int LogStyleLineCount
            {
                get { return Constants.ms_logStyleLineCount; }
                set
                {
                    Constants.ms_logStyleLineCount = value;
                    if (!Constants.ms_Loaded)
                        return;
                    Constants.UpdateLogStyleFixedHeights();
                }
            }

            public static void Init()
            {
                if (ms_Loaded)
                    return;
                Constants.ms_Loaded = true;
                Constants.Box = (GUIStyle) "CN Box";
                Constants.MiniButtonLeft = (GUIStyle) "ToolbarButtonLeft";
                Constants.MiniButton = (GUIStyle) "ToolbarButton";
                Constants.MiniButtonRight = (GUIStyle) "ToolbarButtonRight";
                Constants.Toolbar = (GUIStyle) "Toolbar";
                Constants.LogStyle = (GUIStyle) "CN EntryInfo";
                Constants.LogSmallStyle = (GUIStyle) "CN EntryInfoSmall";
                Constants.WarningStyle = (GUIStyle) "CN EntryWarn";
                Constants.WarningSmallStyle = (GUIStyle) "CN EntryWarnSmall";
                Constants.ErrorStyle = (GUIStyle) "CN EntryError";
                Constants.ErrorSmallStyle = (GUIStyle) "CN EntryErrorSmall";
                Constants.IconLogStyle = (GUIStyle) "CN EntryInfoIcon";
                Constants.IconLogSmallStyle = (GUIStyle) "CN EntryInfoIconSmall";
                Constants.IconWarningStyle = (GUIStyle) "CN EntryWarnIcon";
                Constants.IconWarningSmallStyle = (GUIStyle) "CN EntryWarnIconSmall";
                Constants.IconErrorStyle = (GUIStyle) "CN EntryErrorIcon";
                Constants.IconErrorSmallStyle = (GUIStyle) "CN EntryErrorIconSmall";
                Constants.EvenBackground = (GUIStyle) "CN EntryBackEven";
                Constants.OddBackground = (GUIStyle) "CN EntryBackodd";
                Constants.MessageStyle = (GUIStyle) "CN Message";
                Constants.StatusError = (GUIStyle) "CN StatusError";
                Constants.StatusWarn = (GUIStyle) "CN StatusWarn";
                Constants.StatusLog = (GUIStyle) "CN StatusInfo";
                Constants.CountBadge = (GUIStyle) "CN CountBadge";
                Constants.LogStyleLineCount = EditorPrefs.GetInt("ConsoleWindowLogLineCount", 2);
            }

            private static void UpdateLogStyleFixedHeights()
            {
                Constants.ErrorStyle.fixedHeight =
                    (float) Constants.LogStyleLineCount *
                    Constants.ErrorStyle.lineHeight +
                    (float) Constants.ErrorStyle.border.top;
                Constants.WarningStyle.fixedHeight =
                    (float) Constants.LogStyleLineCount *
                    Constants.WarningStyle.lineHeight +
                    (float) Constants.WarningStyle.border.top;
                Constants.LogStyle.fixedHeight =
                    (float) Constants.LogStyleLineCount * Constants.LogStyle.lineHeight +
                    (float) Constants.LogStyle.border.top;
            }
        }
    }
}