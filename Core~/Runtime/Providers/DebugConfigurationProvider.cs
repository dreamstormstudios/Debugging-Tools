using DebuggingTools.Runtime.Data;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace DebuggingTools.Runtime.Providers
{
    public static class DebugConfigurationProvider
    {
        #region Private Fields

        private static DebugConfiguration configuration;

        #endregion

        #region Constants

        private const string ConfigurationFileName = "DebugConfiguration";
        private const string ConfigurationFilePath = "Assets/Resources/" + ConfigurationFileName;

        #endregion

        #region Public Properties

        public static DebugConfiguration Configuration => IsConfigurationCached() ? configuration : LoadConfiguration();

        #endregion

        #region Private Methods

        private static bool IsConfigurationCached()
        {
            return configuration != null;
        }

        private static DebugConfiguration LoadConfiguration()
        {
#if UNITY_EDITOR
            string configurationPath = $"{ConfigurationFilePath}.asset";

            if (File.Exists(configurationPath))
            {
                return Resources.Load<DebugConfiguration>(ConfigurationFileName);
            }

            if (configuration == null)
            {
                configuration = ScriptableObject.CreateInstance<DebugConfiguration>();
            }

            FileExtensions.CreateDirectoryForFile(configurationPath);
            AssetDatabase.CreateAsset(configuration, configurationPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
            return Resources.Load<DebugConfiguration>(ConfigurationFileName) ?? ScriptableObject.CreateInstance<DebugConfiguration>();
        }

        #endregion
    }
}