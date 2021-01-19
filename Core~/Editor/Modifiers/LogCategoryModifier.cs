using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DebuggingTools.Data;
using DebuggingTools.Runtime;

namespace DebuggingTools.Editor.Modifiers
{
    public class LogCategoryModifier : IHaveLogCategories
    {
        #region Private Fields

        private List<string> allCategories;

        #endregion

        #region Public Properties

        public string[] AllCategories => allCategories.ToArray();

        #endregion

        #region Constructors

        public LogCategoryModifier()
        {
            allCategories = Enum.GetNames(typeof(LogCategory)).ToList();
        }

        #endregion

        #region Public Methods

        public void AddCategory(string elementName)
        {
            if (!IsElementValid(elementName))
            {
                return;
            }

            allCategories.Add(elementName);
        }

        public void RemoveCategory(string elementName)
        {
            if (!ElementWithNameExists(elementName))
            {
                return;
            }

            allCategories.Remove(elementName);
        }

        public void Modify()
        {
            Writer writer = new Writer();

            writer.WriteLine($"namespace {typeof(LogCategory).Namespace}");
            writer.BeginBlock();

            writer.WriteLine($"public enum {nameof(LogCategory)}");
            writer.BeginBlock();

            for (int i = 0; i < allCategories.Count - 1; i++)
            {
                writer.WriteLine($"{allCategories[i]},");
            }

            writer.WriteLine($"{allCategories[allCategories.Count - 1]}");

            writer.EndBlock();
            writer.EndBlock();

            FileExtensions.SaveToFile(writer.Content, GetSavingPath());
        }

        #endregion

        #region Private Methods

        private bool IsElementValid(string elementName)
        {
            return !allCategories.Contains(elementName) && Regex.IsMatch(elementName, @"^[a-zA-Z]+$");
        }

        private bool ElementWithNameExists(string elementName)
        {
            return allCategories.Contains(elementName);
        }

        private string GetSavingPath()
        {
            return $"Assets/Scripts/DebuggingTools/Data/{nameof(LogCategory)}.cs";
        }

        #endregion
    }
}