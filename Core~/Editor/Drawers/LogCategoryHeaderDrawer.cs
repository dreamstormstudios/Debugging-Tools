using UnityEditor;
using UnityEngine;

namespace DebuggingTools.Editor.Drawers
{
    public class LogCategoryHeaderDrawer
    {
        private const float Padding = 50f;
        private const string CategoryHeader = "Log Category";
        private const string EditorHeader = "Editor Options";
        private const string BuildHeader = "Build Options";
        
        public void Draw()
        {
            DrawHeader();
        }
        
        private void DrawHeader()
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);

            EditorGUILayout.BeginHorizontal();

            GUILayoutOption heightOption = GUILayout.MinHeight(35);
            GUILayoutOption widthOption = GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.5f);
            GUILayoutOption halfWidthOption = GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.25f);
            boxStyle.richText = true;

            GUILayout.Space(7);
            GUILayout.Box($"<b><size=15>{CategoryHeader}</size> </b>", boxStyle, widthOption,heightOption);

            GUILayout.BeginHorizontal(widthOption);

            GUILayout.Box($"<b><size=15>{EditorHeader}</size> </b>", boxStyle, halfWidthOption, heightOption);
            GUILayout.Box($"<b><size=15>{BuildHeader}</size> </b>", boxStyle, halfWidthOption, heightOption);
            GUILayout.Space(4);

            GUILayout.EndHorizontal();

            EditorGUILayout.EndHorizontal();
        }
    }
}