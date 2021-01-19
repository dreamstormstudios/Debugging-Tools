using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace DebuggingTools
{
    public static class DebuggingToolsImporter
    {
        #region Private Fields

        private static bool? isPackageImported;
        private static ListRequest list;
        private static Action onListCompleted;

        #endregion

        #region Constants

        private const string CorePackageUrl = "https://github.com/dreamstormstudios/Debugging-Tools.git?path=/Core~";
        private const string CorePackageName = "com.dreamstormstudios.debugging.tools.core";
        private const string AssemblyDefinitionName = LogCategoryNamespace;
        private const string AssemblyDefinitionContent = "{\n\t\"name\": \"" + AssemblyDefinitionName + "\"\n}";
        private const string LogCategoryName = "LogCategory";
        private const string LogCategoryFullName = LogCategoryNamespace + "." + LogCategoryName;
        private const string LogCategoryPath =  LogCategoryDirectory + LogCategoryName + ".cs";
        private const string LogCategoryDirectory = "Assets/Scripts/DebuggingTools/Data/";
        private const string LogCategoryHeader = "public enum " + LogCategoryName;
        private const string LogCategoryNamespace = "DebuggingTools.Data";
        private const string LogCategoryContent = "namespace " + LogCategoryNamespace + "\n{\n\t" + LogCategoryHeader + "\n\t{\n\t\tDefault\n\t}\n}";

        #endregion

        #region Private Methods

        private static void TryCreatingLogCategory()
        {
            if (LogCategoryExists())
            {
                return;
            }

            FileExtensions.SaveToFile(LogCategoryContent, LogCategoryPath);
        }

        private static void TryCreatingAssemblyDefinition()
        {
            if (LogCategoryDirectoryContainsAssemblyDefinition())
            {
                return;
            }

            FileExtensions.SaveToFile(AssemblyDefinitionContent, $"{GetLogCategoryDirectory()}\\{AssemblyDefinitionName}.asmdef");
        }

        private static bool LogCategoryExists()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Any(assembly => assembly.GetTypes().Any(type => type.FullName == LogCategoryFullName));
        }

        private static bool IsLogCategoryInValidAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .Any(assembly => assembly.GetTypes().Any(type => type.FullName == LogCategoryFullName && type.Assembly.GetName().Name == AssemblyDefinitionName));
        }

        private static bool LogCategoryDirectoryContainsAssemblyDefinition()
        {
            string logCategoryDirectory = LogCategoryExists() ? GetLogCategoryDirectory() : LogCategoryDirectory;

            if (!Directory.Exists(logCategoryDirectory))
            {
                return false;
            }

            IEnumerable<string> assemblyDefinitionPaths = Directory.GetFiles(GetLogCategoryDirectory(), "*.asmdef", SearchOption.AllDirectories);

            return assemblyDefinitionPaths.Any();
        }

        private static string GetLogCategoryDirectory()
        {
            IEnumerable<string> scriptPaths = Directory.GetFiles("Assets", "*.cs", SearchOption.AllDirectories);

            foreach (string scriptPath in scriptPaths)
            {
                using (StreamReader reader = new StreamReader(scriptPath))
                {
                    string content = reader.ReadToEnd();

                    if (content.Contains(LogCategoryHeader))
                    {
                        return Path.GetDirectoryName(scriptPath);
                    }
                }
            }

            return string.Empty;
        }

        [DidReloadScripts]
        private static void WaitForScriptCompilation()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += WaitForScriptCompilation;

                return;
            }

            EditorApplication.delayCall += UpdatePackage;
        }

        private static void UpdatePackage()
        {
            if (!isPackageImported.HasValue)
            {
                StartUpdatingPackageState();
            }
        }

        private static void StartUpdatingPackageState()
        {
            EditorUtility.DisplayProgressBar("Debugging tools importer", "Loading packages...", 0f);
            list = Client.List();
            onListCompleted += TryAddingPackage;
            EditorApplication.update += UpdatePackageListing;
        }

        private static void UpdatePackageListing()
        {
            if (list.IsCompleted)
            {
                onListCompleted.Invoke();
                EditorApplication.update -= UpdatePackageListing;
            }
        }

        private static void TryAddingPackage()
        {
            EditorUtility.DisplayProgressBar("Debugging tools importer", "Checking packages...", 0.25f);

            if (!isPackageImported.HasValue)
            {
                UpdatePackageState();
            }

            EditorUtility.DisplayProgressBar("Debugging tools importer", "Preparing package...", 0.5f);

            if (PackageCanBePrepared())
            {
                PreparePackageAddition();
            }

            EditorUtility.DisplayProgressBar("Debugging tools importer", "Adding package...", 1f);

            if (PackageCanBeAdded())
            {
                AddCorePackage();
            }

            EditorUtility.ClearProgressBar();
        }

        private static void PreparePackageAddition()
        {
            PreparePackageForAddition();
            AssetDatabase.Refresh();
        }

        private static void AddCorePackage()
        {
            Client.Add(CorePackageUrl);
        }

        private static void UpdatePackageState()
        {
            isPackageImported = list.Result.Any(package => package.name == CorePackageName);
        }

        private static bool PackageCanBeAdded()
        {
            return LogCategoryExists() && IsLogCategoryInValidAssembly();
        }

        private static bool PackageCanBePrepared()
        {
            return !LogCategoryDirectoryContainsAssemblyDefinition();
        }

        private static void PreparePackageForAddition()
        {
            TryCreatingLogCategory();
            TryCreatingAssemblyDefinition();
        }

        #endregion
    }
}