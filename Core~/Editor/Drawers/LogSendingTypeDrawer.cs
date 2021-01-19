using UnityEditor;
using UnityEngine;
using LogType = DebuggingTools.Runtime.Data.LogType;

namespace DebuggingTools.Editor.Drawers
{
    public class LogSendingTypeDrawer
    {
        #region Private Fields

        private LogType drawnType;
        private bool isEnabledInBuild;
        private bool isEnabledInEditor;

        #endregion

        #region Constants

        private const float Padding = 50f;

        #endregion

        #region Public Properties

        public LogType DrawnType => drawnType;
        public bool IsEnabledInBuild => isEnabledInBuild;
        public bool IsEnabledInEditor => isEnabledInEditor;

        #endregion

        #region Constructors

        public LogSendingTypeDrawer(LogType drawnType, bool isEnabledInBuild, bool isEnabledInEditor)
        {
            this.drawnType = drawnType;
            this.isEnabledInBuild = isEnabledInBuild;
            this.isEnabledInEditor = isEnabledInEditor;
        }

        #endregion

        #region Public Methods

        public void Draw()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(drawnType.ToString(), GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.5f));

            GUILayout.BeginHorizontal(GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.5f));

            isEnabledInEditor = EditorGUILayout.Toggle(isEnabledInEditor);
            isEnabledInBuild = EditorGUILayout.Toggle(isEnabledInBuild);

            GUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}