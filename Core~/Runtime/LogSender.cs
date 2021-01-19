using System;
using DebuggingTools.Runtime.Data;
using DebuggingTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.Analytics;
using LogType = DebuggingTools.Runtime.Data.LogType;

namespace DebuggingTools.Runtime
{
    public static class LogSender
    {
        #region Public Methods

        public static void Send(Log log)
        {
            DebugConfiguration debugConfiguration = DebugConfigurationProvider.Configuration;

            if (CanSendLog(debugConfiguration.FileLogSendingMode))
            {
                SendToFile(log, debugConfiguration.OutputFileDirectory);
            }

            if (CanSendLog(debugConfiguration.AnalyticsLogSendingMode))
            {
                SendToAnalytics(log);
            }

            SendToConsole(log);
        }

        #endregion

        #region Private Methods

        private static bool CanSendLog(LogSendingMode logSendingMode)
        {
            switch (logSendingMode)
            {
                case LogSendingMode.Disabled :
                    return false;
                case LogSendingMode.EditorOnly :
                    return Application.isEditor;
                case LogSendingMode.BuildOnly :
                    return !Application.isEditor;
                case LogSendingMode.EditorAndBuild :
                    return true;
                default :
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SendToAnalytics(Log log)
        {
            Analytics.CustomEvent(log.Description, log.ConvertToDictionary());
        }

        private static void SendToConsole(Log log)
        {
            string logMessage = log.ConvertToString();

            switch (log.Type)
            {
                case LogType.Error :
                    Debug.LogError(logMessage);

                    break;
                case LogType.Warning :
                    Debug.LogWarning(logMessage);

                    break;
                case LogType.Information :
                    Debug.Log(logMessage);

                    break;
                default :
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SendToFile(Log log, string fileDirectory)
        {
            DateTime currentDate = DateTime.Now;
            string date = $"{currentDate.Day}_{currentDate.Month}_{currentDate.Year}";
            string time = $"{currentDate.Hour}:{currentDate.Minute}:{currentDate.Second}.{currentDate.Millisecond}";

            FileExtensions.AppendToFile($"[{time}]\n{log.ConvertToString()}\n", $"{fileDirectory}/{date}");
        }

        #endregion
    }
}