using System;
using System.Collections.Generic;
using System.Linq;
using DebuggingTools.Data;
using DebuggingTools.Editor.Modifiers;
using DebuggingTools.Runtime.Data;
using DebuggingTools.Runtime.Providers;
using UnityEditor;
using UnityEngine;
using LogType = DebuggingTools.Runtime.Data.LogType;

namespace DebuggingTools.Editor.Drawers
{
    public class LogSendingCategoryDrawer : IHaveSendingConfiguration
    {
        #region Private Fields

        private LogCategory drawnCategory;
        private List<LogSendingTypeDrawer> typeConfigurationDrawers = new List<LogSendingTypeDrawer>();

        #endregion

        #region Constants

        private const float Padding = 50f;
        private const string EnabledInBuildText = "Is enabled in build";
        private const string EnabledInEditorText = "Is enabled in editor";

        #endregion

        #region Public Properties

        public string Category => drawnCategory.ToString();

        #endregion

        #region Constructors

        public LogSendingCategoryDrawer(LogCategory drawnCategory)
        {
            this.drawnCategory = drawnCategory;
            ConstructDrawers();
        }

        #endregion

        #region Public Methods

        public void Draw()
        {
            DrawHeader();
            DrawAllTypes();
        }

        public IEnumerable<LogType> GetLogTypesEnabledInEditor()
        {
            return typeConfigurationDrawers.Where(drawer => drawer.IsEnabledInEditor).Select(drawer => drawer.DrawnType);
        }

        public IEnumerable<LogType> GetLogTypesEnabledInBuild()
        {
            return typeConfigurationDrawers.Where(drawer => drawer.IsEnabledInBuild).Select(drawer => drawer.DrawnType);
        }

        #endregion

        #region Private Methods

        private void ConstructDrawers()
        {
            DebugConfiguration debugConfiguration = DebugConfigurationProvider.Configuration;

            List<LogType> logTypesEnabledInEditor = debugConfiguration.LogsSendingDataEnabledInEditor.Where(logSendingData => logSendingData.LogCategory == drawnCategory)
                                                                      .Select(logSendingData => logSendingData.LogType).ToList();
            List<LogType> logTypesEnabledInBuild = debugConfiguration.LogsSendingDataEnabledInBuild.Where(logSendingData => logSendingData.LogCategory == drawnCategory)
                                                                     .Select(logSendingData => logSendingData.LogType).ToList();

            foreach (LogType logType in Enum.GetValues(typeof(LogType)))
            {
                typeConfigurationDrawers.Add(new LogSendingTypeDrawer(logType, logTypesEnabledInBuild.Contains(logType), logTypesEnabledInEditor.Contains(logType)));
            }
        }

        private void DrawHeader()
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);

            EditorGUILayout.BeginHorizontal();

            GUILayoutOption heightOption = GUILayout.MinHeight(35);
            GUILayoutOption widthOption = GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.5f);
            GUILayoutOption halfWidthOption = GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.25f);

            GUILayout.Box(drawnCategory.ToString(), boxStyle, widthOption, heightOption);

            GUILayout.BeginHorizontal(widthOption);

            GUILayout.Box(EnabledInEditorText, boxStyle, halfWidthOption, heightOption);
            GUILayout.Box(EnabledInBuildText, boxStyle, halfWidthOption, heightOption);

            GUILayout.EndHorizontal();

            EditorGUILayout.EndHorizontal();
        }

        private void DrawAllTypes()
        {
            foreach (LogSendingTypeDrawer drawer in typeConfigurationDrawers)
            {
                drawer.Draw();
            }
        }

        #endregion
    }
}