using System.Collections.Generic;
using System.Linq;
using DebuggingTools.Data;
using UnityEngine;

namespace DebuggingTools.Runtime.Data
{
    public class DebugConfiguration : ScriptableObject
    {
        #region Serialized Fields

        [SerializeField]
        private LogSendingMode fileLogSendingMode;
        [SerializeField]
        private LogSendingMode analyticsLogSendingMode;
        [SerializeField]
        private List<LogSendingData> logsSendingDataEnabledInBuild = new List<LogSendingData>();
        [SerializeField]
        private List<LogSendingData> logsSendingDataEnabledInEditor = new List<LogSendingData>();

        #endregion

        #region Public Properties

        public string OutputFileDirectory => $"{Application.persistentDataPath}";
        public LogSendingMode FileLogSendingMode { get => fileLogSendingMode; set => fileLogSendingMode = value; }
        public LogSendingMode AnalyticsLogSendingMode { get => analyticsLogSendingMode; set => analyticsLogSendingMode = value; }
        public List<LogSendingData> LogsSendingDataEnabledInBuild { get => logsSendingDataEnabledInBuild; set => logsSendingDataEnabledInBuild = value; }
        public List<LogSendingData> LogsSendingDataEnabledInEditor { get => logsSendingDataEnabledInEditor; set => logsSendingDataEnabledInEditor = value; }

        #endregion

        #region Public Methods

        public bool CanSendLogOfTypeAndCategory(LogType logType, LogCategory logCategory)
        {
            bool LogSendingDataMatches(LogSendingData logSendingData) => logSendingData.LogType == logType && logSendingData.LogCategory == logCategory;

            return Application.isEditor ? logsSendingDataEnabledInEditor.Any(LogSendingDataMatches) : logsSendingDataEnabledInBuild.Any(LogSendingDataMatches);
        }

        #endregion
    }
}