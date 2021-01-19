using DebuggingTools.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace DebuggingTools.Editor
{
    [CustomEditor(typeof(DebugConfiguration))]
    public class DebugConfigurationEditor : UnityEditor.Editor
    {
        #region Public Methods

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Edit"))
            {
                DebugConfigurationWindow.ShowWindow();
            }
        }

        #endregion
    }
}