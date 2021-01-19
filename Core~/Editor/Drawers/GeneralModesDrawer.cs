using System.Diagnostics;
using System.IO;
using DebuggingTools.Editor.Modifiers;
using DebuggingTools.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace DebuggingTools.Editor.Drawers
{
    public class GeneralModesDrawer : IHaveSendingModes
    {
        #region Private Fields

        private string outputFileDirectory;

        #endregion

        #region Constants

        private const string FileLoggingModeText = "File Logging Mode";
        private const string AnalyticsLoggingModeText = "Analytics Loging Mode";
        private const string OutputFileDirectoryText = "Output Files Directory";
        private const string ShowFileDirectoryInExplorerText = "Show In Explorer";

        #endregion

        #region Public Properties

        public LogSendingMode AnalyticsLogSendingMode { get; private set; }
        public LogSendingMode FileLogSendingMode { get; private set; }

        #endregion

        #region Constructors

        public GeneralModesDrawer(string outputFileDirectory, LogSendingMode analyticsLogSendingMode, LogSendingMode fileLogSendingMode)
        {
            this.outputFileDirectory = outputFileDirectory;
            AnalyticsLogSendingMode = analyticsLogSendingMode;
            FileLogSendingMode = fileLogSendingMode;
        }

        #endregion

        #region Public Methods

        public void Draw()
        {
            EditorGUILayout.BeginHorizontal();

            DrawAnalyticsLoggingModeSelection();
            DrawFileLoggingModeSelection();

            EditorGUILayout.EndHorizontal();

            DrawOutputFileDirectoryBar();
        }

        #endregion

        #region Private Methods

        private void DrawAnalyticsLoggingModeSelection()
        {
            GUILayout.Label(AnalyticsLoggingModeText);
            AnalyticsLogSendingMode = (LogSendingMode) EditorGUILayout.EnumPopup(AnalyticsLogSendingMode);
        }

        private void DrawFileLoggingModeSelection()
        {
            GUILayout.Label(FileLoggingModeText);
            FileLogSendingMode = (LogSendingMode) EditorGUILayout.EnumPopup(FileLogSendingMode);
        }

        private void DrawOutputFileDirectoryBar()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label($"{OutputFileDirectoryText}: {outputFileDirectory}");

            if (Directory.Exists(outputFileDirectory) && GUILayout.Button(ShowFileDirectoryInExplorerText))
            {
                EditorUtility.RevealInFinder(outputFileDirectory);
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}