using System;
using System.Collections.Generic;
using DebuggingTools.Data;
using DebuggingTools.Editor.Drawers;
using DebuggingTools.Editor.Drawers.DebuggingTools.Drawers;
using DebuggingTools.Editor.Modifiers;
using DebuggingTools.Runtime.Data;
using DebuggingTools.Runtime.Providers;
using UnityEditor;
using UnityEngine;

namespace DebuggingTools.Editor
{
    public class DebugConfigurationWindow : EditorWindow
    {
        #region Private Fields

        private Vector2 scrollPosition;
        private LogCategoryDrawer logCategoryDrawer;
        private GeneralModesDrawer generalModesDrawer;
        private LogCategoryModifier logCategoryModifier;
        private DebugConfigurationModifier debugConfigurationModifier;
        private List<LogSendingCategoryDrawer> sendingCategoryDrawers;
        private LogCategoryHeaderDrawer logCategoryHeaderDrawer;

        #endregion

        #region Constants

        private const string ApplyChangesText = "Apply Changes";

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            ConstructServices();
            AssignCallbacks();
        }

        private void OnDisable()
        {
            UnassignCallbacks();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            generalModesDrawer.Draw();

            GUILayout.EndVertical();

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

            logCategoryHeaderDrawer.Draw();
            
            foreach (LogSendingCategoryDrawer drawer in sendingCategoryDrawers)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);

                drawer.Draw();

                GUILayout.EndVertical();
            }

            GUILayout.EndScrollView();

            logCategoryDrawer.Draw();

            if (GUILayout.Button(ApplyChangesText))
            {
                debugConfigurationModifier.Modify();
                logCategoryModifier.Modify();
                AssetDatabase.Refresh();
            }

            GUILayout.EndVertical();
        }

        #endregion

        #region Public Methods

        [MenuItem("Tools/DreamStorm/Debugger Configuration", false, 0)]
        public static void ShowWindow()
        {
            GetWindow<DebugConfigurationWindow>("Debugger Configuration");
        }

        #endregion

        #region Private Methods

        private void ConstructServices()
        {
            DebugConfiguration configuration = DebugConfigurationProvider.Configuration;

            logCategoryModifier = new LogCategoryModifier();
            sendingCategoryDrawers = new List<LogSendingCategoryDrawer>();
            logCategoryDrawer = new LogCategoryDrawer(logCategoryModifier);
            logCategoryHeaderDrawer = new LogCategoryHeaderDrawer();

            foreach (LogCategory logCategory in Enum.GetValues(typeof(LogCategory)))
            {
                sendingCategoryDrawers.Add(new LogSendingCategoryDrawer(logCategory));
            }

            generalModesDrawer = new GeneralModesDrawer(configuration.OutputFileDirectory, configuration.AnalyticsLogSendingMode, configuration.FileLogSendingMode);
            debugConfigurationModifier = new DebugConfigurationModifier(configuration, generalModesDrawer, logCategoryModifier, new List<IHaveSendingConfiguration>(sendingCategoryDrawers));
        }

        private void AssignCallbacks()
        {
            logCategoryDrawer.OnCategoryAdditionTriggered += logCategoryModifier.AddCategory;
            logCategoryDrawer.OnCategoryRemovalTriggered += logCategoryModifier.RemoveCategory;
        }

        private void UnassignCallbacks()
        {
            logCategoryDrawer.OnCategoryAdditionTriggered -= logCategoryModifier.AddCategory;
            logCategoryDrawer.OnCategoryRemovalTriggered -= logCategoryModifier.RemoveCategory;
        }

        #endregion
    }
}