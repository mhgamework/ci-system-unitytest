// Decompiled with JetBrains decompiler
// Type: UnityEditor.ConsoleWindow
// Assembly: UnityEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17B72532-EE2C-4DA1-B6F6-32D3F1705FE0
// Assembly location: C:\Program Files\Unity\Hub\Editor\2019.3.10f1\Editor\Data\Managed\UnityEditor.dll

using JetBrains.Annotations;
using System;
using System.Globalization;
using System.Text;
using Unity.MPE;
using UnityEditor.Experimental.Networking.PlayerConnection;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.Networking.PlayerConnection;
using UnityEngine.Scripting;

namespace UnityEditor
{
  internal class ConsoleWindow2 : EditorWindow/*, IHasCustomMenu*/
   {
//     private static bool ms_LoadedIcons = false;
//     private static ConsoleWindow2 ms_ConsoleWindow = (ConsoleWindow2) null;
//     private string m_ActiveText = "";
//     private int m_ActiveInstanceID = 0;
//     private Vector2 m_TextScroll = Vector2.zero;
//     // private SplitterState spl = new SplitterState(new float[2]
//     // {
//     //   70f,
//     //   30f
//     // }, new int[2]{ 32, 32 }, (int[]) null);
//     private int ms_LVHeight = 0;
//     private int m_LineHeight;
//     private int m_BorderHeight;
//     private bool m_HasUpdatedGuiStyles;
//     private ListViewState m_ListView;
//     private bool m_DevBuild;
//     internal static Texture2D iconInfo;
//     internal static Texture2D iconWarn;
//     internal static Texture2D iconError;
//     internal static Texture2D iconInfoSmall;
//     internal static Texture2D iconWarnSmall;
//     internal static Texture2D iconErrorSmall;
//     internal static Texture2D iconInfoMono;
//     internal static Texture2D iconWarnMono;
//     internal static Texture2D iconErrorMono;
//     private IConnectionState m_ConsoleAttachToPlayerState;
//     private string m_SearchText;
//
//     public static void ShowConsoleWindow(bool immediate)
//     {
//       if ((UnityEngine.Object) ConsoleWindow2.ms_ConsoleWindow == (UnityEngine.Object) null)
//       {
//         ConsoleWindow2.ms_ConsoleWindow = ScriptableObject.CreateInstance<ConsoleWindow2>();
//         if (ProcessService.level == ProcessLevel.UMP_MASTER)
//           ConsoleWindow2.ms_ConsoleWindow.Show(immediate);
//         else
//           ConsoleWindow2.ms_ConsoleWindow.ShowModalUtility();
//         ConsoleWindow2.ms_ConsoleWindow.Focus();
//       }
//       else
//       {
//         ConsoleWindow2.ms_ConsoleWindow.Show(immediate);
//         ConsoleWindow2.ms_ConsoleWindow.Focus();
//       }
//     }
//
//     internal static void LoadIcons()
//     {
//       if (ConsoleWindow2.ms_LoadedIcons)
//         return;
//       ConsoleWindow2.ms_LoadedIcons = true;
//       ConsoleWindow2.iconInfo = EditorGUIUtility.LoadIcon("console.infoicon");
//       ConsoleWindow2.iconWarn = EditorGUIUtility.LoadIcon("console.warnicon");
//       ConsoleWindow2.iconError = EditorGUIUtility.LoadIcon("console.erroricon");
//       ConsoleWindow2.iconInfoSmall = EditorGUIUtility.LoadIcon("console.infoicon.sml");
//       ConsoleWindow2.iconWarnSmall = EditorGUIUtility.LoadIcon("console.warnicon.sml");
//       ConsoleWindow2.iconErrorSmall = EditorGUIUtility.LoadIcon("console.erroricon.sml");
//       ConsoleWindow2.iconInfoMono = EditorGUIUtility.LoadIcon("console.infoicon.inactive.sml");
//       ConsoleWindow2.iconWarnMono = EditorGUIUtility.LoadIcon("console.warnicon.inactive.sml");
//       ConsoleWindow2.iconErrorMono = EditorGUIUtility.LoadIcon("console.erroricon.inactive.sml");
//       ConsoleWindow2.Constants.Init();
//     }
//
//  
//     public ConsoleWindow2()
//     {
//       this.position = new Rect(200f, 200f, 800f, 400f);
//       this.m_ListView = new ListViewState(0, 0);
//       this.m_SearchText = string.Empty;
//       EditorGUI.hyperLinkClicked += new EventHandler(this.EditorGUI_HyperLinkClicked);
//     }
//
//     internal void OnEnable()
//     {
//       if (this.m_ConsoleAttachToPlayerState == null)
//         this.m_ConsoleAttachToPlayerState = (IConnectionState) new ConsoleWindow2.ConsoleAttachToPlayerState((EditorWindow) this, (Action<string>) null);
//       this.SetFilter(LogEntries.GetFilteringText());
//       this.titleContent = this.GetLocalizedTitleContent();
//       ConsoleWindow2.ms_ConsoleWindow = this;
//       this.m_DevBuild = Unsupported.IsDeveloperMode();
//       ConsoleWindow2.Constants.LogStyleLineCount = EditorPrefs.GetInt("ConsoleWindowLogLineCount", 2);
//     }
//
//     internal void OnDisable()
//     {
//       this.m_ConsoleAttachToPlayerState?.Dispose();
//       this.m_ConsoleAttachToPlayerState = (IConnectionState) null;
//       if (!((UnityEngine.Object) ConsoleWindow2.ms_ConsoleWindow == (UnityEngine.Object) this))
//         return;
//       ConsoleWindow2.ms_ConsoleWindow = (ConsoleWindow2) null;
//     }
//
//     private int RowHeight
//     {
//       get
//       {
//         return (ConsoleWindow2.Constants.LogStyleLineCount > 1 ? Mathf.Max(32, ConsoleWindow2.Constants.LogStyleLineCount * this.m_LineHeight) : this.m_LineHeight) + this.m_BorderHeight;
//       }
//     }
//
//     private static bool HasMode(int mode, ConsoleWindow2.Mode modeToCheck)
//     {
//       return (uint) ((ConsoleWindow2.Mode) mode & modeToCheck) > 0U;
//     }
//
//     private static bool HasFlag(ConsoleWindow2.ConsoleFlags flags)
//     {
//       return (uint) ((ConsoleWindow2.ConsoleFlags) LogEntries.consoleFlags & flags) > 0U;
//     }
//
//     private static void SetFlag(ConsoleWindow2.ConsoleFlags flags, bool val)
//     {
//       LogEntries.SetConsoleFlag((int) flags, val);
//     }
//
//     internal static Texture2D GetIconForErrorMode(int mode, bool large)
//     {
//       if (ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.Error | ConsoleWindow2.Mode.Assert | ConsoleWindow2.Mode.Fatal | ConsoleWindow2.Mode.AssetImportError | ConsoleWindow2.Mode.ScriptingError | ConsoleWindow2.Mode.ScriptCompileError | ConsoleWindow2.Mode.GraphCompileError | ConsoleWindow2.Mode.ScriptingAssertion))
//         return large ? ConsoleWindow2.iconError : ConsoleWindow2.iconErrorSmall;
//       if (ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.AssetImportWarning | ConsoleWindow2.Mode.ScriptingWarning | ConsoleWindow2.Mode.ScriptCompileWarning))
//         return large ? ConsoleWindow2.iconWarn : ConsoleWindow2.iconWarnSmall;
//       return ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.Log | ConsoleWindow2.Mode.ScriptingLog) ? (large ? ConsoleWindow2.iconInfo : ConsoleWindow2.iconInfoSmall) : (Texture2D) null;
//     }
//
     // internal static GUIStyle GetStyleForErrorMode(int mode, bool isIcon, bool isSmall)
     // {
     //   return ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.Error | ConsoleWindow2.Mode.Assert | ConsoleWindow2.Mode.Fatal | ConsoleWindow2.Mode.AssetImportError | ConsoleWindow2.Mode.ScriptingError | ConsoleWindow2.Mode.ScriptCompileError | ConsoleWindow2.Mode.ScriptingException | ConsoleWindow2.Mode.GraphCompileError | ConsoleWindow2.Mode.ScriptingAssertion) ? (isIcon ? (isSmall ? ConsoleWindow2.Constants.IconErrorSmallStyle : ConsoleWindow2.Constants.IconErrorStyle) : (isSmall ? ConsoleWindow2.Constants.ErrorSmallStyle : ConsoleWindow2.Constants.ErrorStyle)) : (ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.AssetImportWarning | ConsoleWindow2.Mode.ScriptingWarning | ConsoleWindow2.Mode.ScriptCompileWarning) ? (isIcon ? (isSmall ? ConsoleWindow2.Constants.IconWarningSmallStyle : ConsoleWindow2.Constants.IconWarningStyle) : (isSmall ? ConsoleWindow2.Constants.WarningSmallStyle : ConsoleWindow2.Constants.WarningStyle)) : (isIcon ? (isSmall ? ConsoleWindow2.Constants.IconLogSmallStyle : ConsoleWindow2.Constants.IconLogStyle) : (isSmall ? ConsoleWindow2.Constants.LogSmallStyle : ConsoleWindow2.Constants.LogStyle)));
     // }
//
//     internal static GUIStyle GetStatusStyleForErrorMode(int mode)
//     {
//       if (ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.Error | ConsoleWindow2.Mode.Assert | ConsoleWindow2.Mode.Fatal | ConsoleWindow2.Mode.AssetImportError | ConsoleWindow2.Mode.ScriptingError | ConsoleWindow2.Mode.ScriptCompileError | ConsoleWindow2.Mode.GraphCompileError | ConsoleWindow2.Mode.ScriptingAssertion))
//         return ConsoleWindow2.Constants.StatusError;
//       return ConsoleWindow2.HasMode(mode, ConsoleWindow2.Mode.AssetImportWarning | ConsoleWindow2.Mode.ScriptingWarning | ConsoleWindow2.Mode.ScriptCompileWarning) ? ConsoleWindow2.Constants.StatusWarn : ConsoleWindow2.Constants.StatusLog;
//     }
//
//     private void SetActiveEntry(LogEntry entry)
//     {
//       if (entry != null)
//       {
//         this.m_ActiveText = entry.message;
//         if (this.m_ActiveInstanceID == entry.instanceID)
//           return;
//         this.m_ActiveInstanceID = entry.instanceID;
//         if ((uint) entry.instanceID > 0U)
//           EditorGUIUtility.PingObject(entry.instanceID);
//       }
//       else
//       {
//         this.m_ActiveText = string.Empty;
//         this.m_ActiveInstanceID = 0;
//         this.m_ListView.row = -1;
//       }
//     }
//
//     [UsedImplicitly]
//     internal static void ShowConsoleRow(int row)
//     {
//       ConsoleWindow2.ShowConsoleWindow(false);
//       if (!(bool) (UnityEngine.Object) ConsoleWindow2.ms_ConsoleWindow)
//         return;
//       ConsoleWindow2.ms_ConsoleWindow.m_ListView.row = row;
//       ConsoleWindow2.ms_ConsoleWindow.m_ListView.selectionChanged = true;
//       ConsoleWindow2.ms_ConsoleWindow.Repaint();
//     }
//
//     private void UpdateListView()
//     {
//       this.m_HasUpdatedGuiStyles = true;
//       int rowHeight = this.RowHeight;
//       this.m_ListView.rowHeight = rowHeight;
//       this.m_ListView.row = -1;
//       this.m_ListView.scrollPos.y = (float) (LogEntries.GetCount() * rowHeight);
//     }
//
//     internal void OnGUI()
//     {
//       Event current = Event.current;
//       ConsoleWindow2.LoadIcons();
//       if (!this.m_HasUpdatedGuiStyles)
//       {
//         this.m_LineHeight = Mathf.RoundToInt(ConsoleWindow2.Constants.ErrorStyle.lineHeight);
//         this.m_BorderHeight = ConsoleWindow2.Constants.ErrorStyle.border.top + ConsoleWindow2.Constants.ErrorStyle.border.bottom;
//         this.UpdateListView();
//       }
//     SplitterGUILayout.BeginVerticalSplit(this.spl);
//       GUIContent content = new GUIContent();
//       int controlId = GUIUtility.GetControlID(FocusType.Native);
//       int index = -1;
//       using (new GettingLogEntriesScope(this.m_ListView))
//       {
//         int num1 = -1;
//         bool isDoubleClicked = false;
//         foreach (ListViewElement listViewElement in ListViewGUI.ListView(this.m_ListView, ConsoleWindow2.Constants.Box))
//         {
//           if (current.type == UnityEngine.EventType.MouseDown && current.button == 0 && listViewElement.position.Contains(current.mousePosition))
//           {
//             num1 = this.m_ListView.row;
//             if (current.clickCount == 2)
//               isDoubleClicked = true;
//           }
//           else if (current.type == UnityEngine.EventType.Repaint)
//           {
//             int mask = 0;
//             string outString = (string) null;
//             LogEntries.GetLinesAndModeFromEntryInternal(listViewElement.row, ConsoleWindow2.Constants.LogStyleLineCount, ref mask, ref outString);
//             int num2 = ConsoleWindow2.Constants.LogStyleLineCount == 1 ? 4 : 8;
//             (listViewElement.row % 2 == 0 ? ConsoleWindow2.Constants.OddBackground : ConsoleWindow2.Constants.EvenBackground).Draw(listViewElement.position, false, false, this.m_ListView.row == listViewElement.row, false);
//             GUIStyle styleForErrorMode1 = ConsoleWindow2.GetStyleForErrorMode(mask, true, ConsoleWindow2.Constants.LogStyleLineCount == 1);
//             Rect position1 = listViewElement.position;
//             position1.x += (float) num2;
//             position1.y += 2f;
//             styleForErrorMode1.Draw(position1, false, false, this.m_ListView.row == listViewElement.row, false);
//             content.text = outString;
//             GUIStyle styleForErrorMode2 = ConsoleWindow2.GetStyleForErrorMode(mask, false, ConsoleWindow2.Constants.LogStyleLineCount == 1);
//             Rect position2 = listViewElement.position;
//             position2.x += (float) num2;
//             if (string.IsNullOrEmpty(this.m_SearchText))
//             {
//               styleForErrorMode2.Draw(position2, content, controlId, this.m_ListView.row == listViewElement.row);
//             }
//             else
//             {
//               int firstSelectedCharacter = outString.IndexOf(this.m_SearchText, StringComparison.OrdinalIgnoreCase);
//               if (firstSelectedCharacter == -1)
//               {
//                 styleForErrorMode2.Draw(position2, content, controlId, this.m_ListView.row == listViewElement.row);
//               }
//               else
//               {
//                 int lastSelectedCharacter = firstSelectedCharacter + this.m_SearchText.Length;
//                 Color selectionColor = GUI.skin.settings.selectionColor;
//                 styleForErrorMode2.DrawWithTextSelection(position2, content, false, true, firstSelectedCharacter, lastSelectedCharacter, false, selectionColor);
//               }
//             }
//           }
//         }
//         if (num1 != -1 && (double) this.m_ListView.scrollPos.y >= (double) (this.m_ListView.rowHeight * this.m_ListView.totalRows - this.ms_LVHeight))
//           this.m_ListView.scrollPos.y = (float) (this.m_ListView.rowHeight * this.m_ListView.totalRows - this.ms_LVHeight - 1);
//         if (this.m_ListView.totalRows == 0 || this.m_ListView.row >= this.m_ListView.totalRows || this.m_ListView.row < 0)
//         {
//           if ((uint) this.m_ActiveText.Length > 0U)
//             this.SetActiveEntry((LogEntry) null);
//         }
//         else
//         {
//           LogEntry logEntry = new LogEntry();
//           LogEntries.GetEntryInternal(this.m_ListView.row, logEntry);
//           this.SetActiveEntry(logEntry);
//           LogEntries.GetEntryInternal(this.m_ListView.row, logEntry);
//           if (this.m_ListView.selectionChanged || !this.m_ActiveText.Equals(logEntry.message))
//             this.SetActiveEntry(logEntry);
//         }
//         if (GUIUtility.keyboardControl == this.m_ListView.ID && current.type == UnityEngine.EventType.KeyDown && current.keyCode == KeyCode.Return && (uint) this.m_ListView.row > 0U)
//         {
//           num1 = this.m_ListView.row;
//           isDoubleClicked = true;
//         }
//         if (current.type != UnityEngine.EventType.Layout && ListViewGUI.ilvState.rectHeight != 1)
//           this.ms_LVHeight = ListViewGUI.ilvState.rectHeight;
//         if (isDoubleClicked)
//         {
//           index = num1;
//           current.Use();
//         }
//       }
//       if (index != -1)
//         LogEntries.RowGotDoubleClicked(index);
//       this.m_TextScroll = GUILayout.BeginScrollView(this.m_TextScroll, ConsoleWindow2.Constants.Box);
//       string stacktraceWithHyperlinks = ConsoleWindow2.StacktraceWithHyperlinks(this.m_ActiveText);
//       float num = ConsoleWindow2.Constants.MessageStyle.CalcHeight(GUIContent.Temp(stacktraceWithHyperlinks), this.position.width);
//       EditorGUILayout.SelectableLabel(stacktraceWithHyperlinks, ConsoleWindow2.Constants.MessageStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true), GUILayout.MinHeight(num + 10f));
//       GUILayout.EndScrollView();
//       SplitterGUILayout.EndVerticalSplit();
//       if (current.type != UnityEngine.EventType.ValidateCommand && current.type != UnityEngine.EventType.ExecuteCommand || !(current.commandName == "Copy") || !(this.m_ActiveText != string.Empty))
//         return;
//       if (current.type == UnityEngine.EventType.ExecuteCommand)
//         EditorGUIUtility.systemCopyBuffer = this.m_ActiveText;
//       current.Use();
//     }
//
//     
//     internal static string StacktraceWithHyperlinks(string stacktraceText)
//     {
//       StringBuilder stringBuilder = new StringBuilder();
//       string[] strArray = stacktraceText.Split(new string[1]
//       {
//         "\n"
//       }, StringSplitOptions.None);
//       for (int index1 = 0; index1 < strArray.Length; ++index1)
//       {
//         string str1 = ") (at ";
//         int num1 = strArray[index1].IndexOf(str1, StringComparison.Ordinal);
//         if (num1 > 0)
//         {
//           int index2 = num1 + str1.Length;
//           if (strArray[index1][index2] != '<')
//           {
//             string str2 = strArray[index1].Substring(index2);
//             int length = str2.LastIndexOf(":", StringComparison.Ordinal);
//             if (length > 0)
//             {
//               int num2 = str2.LastIndexOf(")", StringComparison.Ordinal);
//               if (num2 > 0)
//               {
//                 string str3 = str2.Substring(length + 1, num2 - (length + 1));
//                 string str4 = str2.Substring(0, length);
//                 stringBuilder.Append(strArray[index1].Substring(0, index2));
//                 stringBuilder.Append("<a href=\"" + str4 + "\" line=\"" + str3 + "\">");
//                 stringBuilder.Append(str4 + ":" + str3);
//                 stringBuilder.Append("</a>)\n");
//                 continue;
//               }
//             }
//           }
//         }
//         stringBuilder.Append(strArray[index1] + "\n");
//       }
//       if (stringBuilder.Length > 0)
//         stringBuilder.Remove(stringBuilder.Length - 1, 1);
//       return stringBuilder.ToString();
//     }
//
//     private void EditorGUI_HyperLinkClicked(object sender, EventArgs e)
//     {
//       EditorGUILayout.HyperLinkClickedEventArgs clickedEventArgs = (EditorGUILayout.HyperLinkClickedEventArgs) e;
//       string filePath;
//       string s;
//       if (!clickedEventArgs.hyperlinkInfos.TryGetValue("href", out filePath) || !clickedEventArgs.hyperlinkInfos.TryGetValue("line", out s))
//         return;
//       int line = int.Parse(s);
//       if (string.IsNullOrEmpty(filePath.Replace('\\', '/')))
//         return;
//       LogEntries.OpenFileOnSpecificLineAndColumn(filePath, line, -1);
//     }
//     
//
//     [UsedImplicitly]
//     private static event ConsoleWindow2.EntryDoubleClickedDelegate entryWithManagedCallbackDoubleClicked;
//
//     [RequiredByNativeCode]
//     [UsedImplicitly]
//     private static void SendEntryDoubleClicked(LogEntry entry)
//     {
//       ConsoleWindow2.EntryDoubleClickedDelegate callbackDoubleClicked = ConsoleWindow2.entryWithManagedCallbackDoubleClicked;
//       if (callbackDoubleClicked == null)
//         return;
//       callbackDoubleClicked(entry);
//     }
//
//     [UsedImplicitly]
//     internal void AddMessageWithDoubleClickCallback(
//       string message,
//       string file,
//       int mode,
//       int instanceID)
//     {
//       LogEntries.AddMessageWithDoubleClickCallback(new LogEntry()
//       {
//         message = message,
//         file = file,
//         mode = mode,
//         instanceID = instanceID
//       });
//     }
//
//     private void SetFilter(string filteringText)
//     {
//       if (filteringText == null)
//       {
//         this.m_SearchText = "";
//         LogEntries.SetFilteringText("");
//       }
//       else
//       {
//         this.m_SearchText = filteringText;
//         LogEntries.SetFilteringText(filteringText);
//       }
//       this.SetActiveEntry((LogEntry) null);
//     }
//
//     internal delegate void EntryDoubleClickedDelegate(LogEntry entry);

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
         get
         {
           return ConsoleWindow2.Constants.ms_logStyleLineCount;
         }
         set
         {
           ConsoleWindow2.Constants.ms_logStyleLineCount = value;
           if (!ConsoleWindow2.Constants.ms_Loaded)
             return;
           ConsoleWindow2.Constants.UpdateLogStyleFixedHeights();
         }
       }

       public static void Init()
       {
         if (ConsoleWindow2.Constants.ms_Loaded)
           return;
         ConsoleWindow2.Constants.ms_Loaded = true;
         ConsoleWindow2.Constants.Box = (GUIStyle) "CN Box";
         ConsoleWindow2.Constants.MiniButtonLeft = (GUIStyle) "ToolbarButtonLeft";
         ConsoleWindow2.Constants.MiniButton = (GUIStyle) "ToolbarButton";
         ConsoleWindow2.Constants.MiniButtonRight = (GUIStyle) "ToolbarButtonRight";
         ConsoleWindow2.Constants.Toolbar = (GUIStyle) "Toolbar";
         ConsoleWindow2.Constants.LogStyle = (GUIStyle) "CN EntryInfo";
         ConsoleWindow2.Constants.LogSmallStyle = (GUIStyle) "CN EntryInfoSmall";
         ConsoleWindow2.Constants.WarningStyle = (GUIStyle) "CN EntryWarn";
         ConsoleWindow2.Constants.WarningSmallStyle = (GUIStyle) "CN EntryWarnSmall";
         ConsoleWindow2.Constants.ErrorStyle = (GUIStyle) "CN EntryError";
         ConsoleWindow2.Constants.ErrorSmallStyle = (GUIStyle) "CN EntryErrorSmall";
         ConsoleWindow2.Constants.IconLogStyle = (GUIStyle) "CN EntryInfoIcon";
         ConsoleWindow2.Constants.IconLogSmallStyle = (GUIStyle) "CN EntryInfoIconSmall";
         ConsoleWindow2.Constants.IconWarningStyle = (GUIStyle) "CN EntryWarnIcon";
         ConsoleWindow2.Constants.IconWarningSmallStyle = (GUIStyle) "CN EntryWarnIconSmall";
         ConsoleWindow2.Constants.IconErrorStyle = (GUIStyle) "CN EntryErrorIcon";
         ConsoleWindow2.Constants.IconErrorSmallStyle = (GUIStyle) "CN EntryErrorIconSmall";
         ConsoleWindow2.Constants.EvenBackground = (GUIStyle) "CN EntryBackEven";
         ConsoleWindow2.Constants.OddBackground = (GUIStyle) "CN EntryBackodd";
         ConsoleWindow2.Constants.MessageStyle = (GUIStyle) "CN Message";
         ConsoleWindow2.Constants.StatusError = (GUIStyle) "CN StatusError";
         ConsoleWindow2.Constants.StatusWarn = (GUIStyle) "CN StatusWarn";
         ConsoleWindow2.Constants.StatusLog = (GUIStyle) "CN StatusInfo";
         ConsoleWindow2.Constants.CountBadge = (GUIStyle) "CN CountBadge";
         ConsoleWindow2.Constants.LogStyleLineCount = EditorPrefs.GetInt("ConsoleWindowLogLineCount", 2);
       }

       private static void UpdateLogStyleFixedHeights()
       {
         ConsoleWindow2.Constants.ErrorStyle.fixedHeight = (float) ConsoleWindow2.Constants.LogStyleLineCount * ConsoleWindow2.Constants.ErrorStyle.lineHeight + (float) ConsoleWindow2.Constants.ErrorStyle.border.top;
         ConsoleWindow2.Constants.WarningStyle.fixedHeight = (float) ConsoleWindow2.Constants.LogStyleLineCount * ConsoleWindow2.Constants.WarningStyle.lineHeight + (float) ConsoleWindow2.Constants.WarningStyle.border.top;
         ConsoleWindow2.Constants.LogStyle.fixedHeight = (float) ConsoleWindow2.Constants.LogStyleLineCount * ConsoleWindow2.Constants.LogStyle.lineHeight + (float) ConsoleWindow2.Constants.LogStyle.border.top;
       }
     }

//    
//
//     [Flags]
//     internal enum Mode
//     {
//       Error = 1,
//       Assert = 2,
//       Log = 4,
//       Fatal = 16, // 0x00000010
//       DontPreprocessCondition = 32, // 0x00000020
//       AssetImportError = 64, // 0x00000040
//       AssetImportWarning = 128, // 0x00000080
//       ScriptingError = 256, // 0x00000100
//       ScriptingWarning = 512, // 0x00000200
//       ScriptingLog = 1024, // 0x00000400
//       ScriptCompileError = 2048, // 0x00000800
//       ScriptCompileWarning = 4096, // 0x00001000
//       StickyError = 8192, // 0x00002000
//       MayIgnoreLineNumber = 16384, // 0x00004000
//       ReportBug = 32768, // 0x00008000
//       DisplayPreviousErrorInStatusBar = 65536, // 0x00010000
//       ScriptingException = 131072, // 0x00020000
//       DontExtractStacktrace = 262144, // 0x00040000
//       ShouldClearOnPlay = 524288, // 0x00080000
//       GraphCompileError = 1048576, // 0x00100000
//       ScriptingAssertion = 2097152, // 0x00200000
//       VisualScriptingError = 4194304, // 0x00400000
//     }
//
//     public struct StackTraceLogTypeData
//     {
//       public LogType logType;
//       public StackTraceLogType stackTraceLogType;
//     }
   }
}
