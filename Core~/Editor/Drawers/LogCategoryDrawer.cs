using System;
using DebuggingTools.Editor.Modifiers;
using UnityEditor;
using UnityEngine;

namespace DebuggingTools.Editor.Drawers
{
    namespace DebuggingTools.Drawers
    {
        public class LogCategoryDrawer
        {
            #region Events

            public event Action<string> OnCategoryAdditionTriggered;
            public event Action<string> OnCategoryRemovalTriggered;

            #endregion

            #region Private Fields

            private bool isExpanded;
            private string newCategory;
            private Vector2 scrollPosition;
            private IHaveLogCategories haveLogCategories;

            #endregion

            #region Constants

            private const int Padding = 40;
            private const int DefaultCategoryIndex = 0;
            private const string AddText = "Add Category";
            private const string RemoveText = "Remove Category";
            private const string HideText = "Hide Log Categories";
            private const string ShowText = "Show Log Categories";

            #endregion

            #region Constructors

            public LogCategoryDrawer(IHaveLogCategories haveLogCategories)
            {
                this.haveLogCategories = haveLogCategories;
            }

            #endregion

            #region Public Methods

            public void Draw()
            {
                HandleTogglingView();

                if (!isExpanded)
                {
                    return;
                }

                HandleCategoryAddition();

                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

                HandleCategoryClear();

                GUILayout.EndScrollView();
            }

            #endregion

            #region Private Methods

            private void HandleTogglingView()
            {
                if (GUILayout.Button(isExpanded ? HideText : ShowText))
                {
                    isExpanded = !isExpanded;
                }
            }

            private void HandleCategoryAddition()
            {
                EditorGUILayout.BeginHorizontal();

                newCategory = EditorGUILayout.TextArea(newCategory, GUILayout.Width(EditorGUIUtility.currentViewWidth / 2f));

                if (GUILayout.Button(AddText))
                {
                    if (string.IsNullOrWhiteSpace(newCategory))
                    {
                        return;
                    }

                    OnCategoryAdditionTriggered?.Invoke(newCategory);
                }

                EditorGUILayout.EndHorizontal();
            }

            private void HandleCategoryClear()
            {
                foreach (string elementName in haveLogCategories.AllCategories)
                {
                    HandleCategoryRemoval(elementName);
                }
            }

            private void HandleCategoryRemoval(string category)
            {
                bool isDefaultCategory = Array.IndexOf(haveLogCategories.AllCategories, category) == DefaultCategoryIndex;
                float windowWidthModifier = isDefaultCategory ? 1f : 0.8f;
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);

                EditorGUILayout.BeginHorizontal();

                GUILayout.Box(category, boxStyle, GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * windowWidthModifier));

                if (isDefaultCategory)
                {
                    EditorGUILayout.EndHorizontal();

                    return;
                }

                if (GUILayout.Button(RemoveText, GUILayout.Width((EditorGUIUtility.currentViewWidth - Padding) * 0.195f)))
                {
                    OnCategoryRemovalTriggered?.Invoke(category);
                }

                EditorGUILayout.EndHorizontal();
            }

            #endregion
        }
    }
}