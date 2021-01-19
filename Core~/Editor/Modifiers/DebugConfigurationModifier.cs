using System;
using System.Collections.Generic;
using System.Linq;
using DebuggingTools.Data;
using DebuggingTools.Runtime.Data;
using UnityEditor;

namespace DebuggingTools.Editor.Modifiers
{
    public class DebugConfigurationModifier
    {
        #region Private Fields

        private DebugConfiguration configuration;
        private IHaveSendingModes haveSendingModes;
        private IHaveLogCategories haveLogCategories;
        private List<IHaveSendingConfiguration> haveSendingConfigurations;

        #endregion

        #region Constructors

        public DebugConfigurationModifier(DebugConfiguration configuration, IHaveSendingModes haveSendingModes, IHaveLogCategories haveLogCategories,
                                          List<IHaveSendingConfiguration> haveSendingConfigurations)
        {
            this.configuration = configuration;
            this.haveSendingModes = haveSendingModes;
            this.haveLogCategories = haveLogCategories;
            this.haveSendingConfigurations = haveSendingConfigurations;
        }

        #endregion

        #region Public Methods

        public void Modify()
        {
            ApplySendingConfiguration();
            ApplySendingModes();

            EditorUtility.SetDirty(configuration);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion

        #region Private Methods

        private void ApplySendingConfiguration()
        {
            configuration.LogsSendingDataEnabledInEditor = GetLogSendingData(GetLogTypesEnabledInEditor);
            configuration.LogsSendingDataEnabledInBuild = GetLogSendingData(GetLogTypesEnabledInBuild);
        }

        private void ApplySendingModes()
        {
            configuration.AnalyticsLogSendingMode = haveSendingModes.AnalyticsLogSendingMode;
            configuration.FileLogSendingMode = haveSendingModes.FileLogSendingMode;
        }

        private List<LogSendingData> GetLogSendingData(Func<IHaveSendingConfiguration, IEnumerable<LogType>> getEnabledLogTypes)
        {
            List<LogSendingData> sendingData = new List<LogSendingData>();

            foreach (IHaveSendingConfiguration haveSendingConfiguration in haveSendingConfigurations)
            {
                if (haveLogCategories.AllCategories.All(category => haveSendingConfiguration.Category != category))
                {
                    continue;
                }

                LogCategory logCategory = (LogCategory) Array.IndexOf(haveLogCategories.AllCategories, haveSendingConfiguration.Category);

                sendingData.AddRange(getEnabledLogTypes.Invoke(haveSendingConfiguration).Select(logType => new LogSendingData(logType, logCategory)));
            }

            return sendingData;
        }

        private IEnumerable<LogType> GetLogTypesEnabledInEditor(IHaveSendingConfiguration haveSendingConfiguration)
        {
            return haveSendingConfiguration.GetLogTypesEnabledInEditor();
        }

        private IEnumerable<LogType> GetLogTypesEnabledInBuild(IHaveSendingConfiguration haveSendingConfiguration)
        {
            return haveSendingConfiguration.GetLogTypesEnabledInBuild();
        }

        #endregion
    }
}