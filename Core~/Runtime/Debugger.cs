using System;
using System.Collections.Generic;
using DebuggingTools.Data;
using DebuggingTools.Runtime.Data;
using DebuggingTools.Runtime.Interfaces;
using DebuggingTools.Runtime.Providers;
using UnityEngine;
using LogType = DebuggingTools.Runtime.Data.LogType;

namespace DebuggingTools.Runtime
{
    /// <summary>
    ///     Class responsible for:
    ///     - providing debug methods
    /// </summary>
    public static class Debugger
    {
        #region Public Methods

        public static void Log(string message, LogCategory category, params object[] args)
        {
            Log(message, LogType.Information, category, ConvertToObjectLogs(args));
        }

        public static void Log(string message, params object[] args)
        {
            Log(message, LogType.Information, LogCategory.Default, ConvertToObjectLogs(args));
        }

        public static void LogError(string message, LogCategory category, params object[] args)
        {
            Log(message, LogType.Error, category, ConvertToObjectLogs(args));
        }

        public static void LogError(string message, params object[] args)
        {
            Log(message, LogType.Error, LogCategory.Default, ConvertToObjectLogs(args));
        }

        public static void LogWarning(string message, LogCategory category, params object[] args)
        {
            Log(message, LogType.Warning, category, ConvertToObjectLogs(args));
        }

        public static void LogWarning(string message, params object[] args)
        {
            Log(message, LogType.Warning, LogCategory.Default, ConvertToObjectLogs(args));
        }

        #endregion

        #region Private Methods

        private static void Log(string message, LogType type, LogCategory category, ObjectLog[] objectLogs)
        {
            DebugConfiguration debugConfiguration = DebugConfigurationProvider.Configuration;

            try
            {
                if (debugConfiguration.CanSendLogOfTypeAndCategory(type, category))
                {
                    Log log = new Log(message, type, objectLogs);
                    LogSender.Send(log);
                }
            }
            catch (Exception caughtException)
            {
                Debug.Log(caughtException);

                throw;
            }
        }

        private static ObjectLog[] ConvertToObjectLogs(IEnumerable<object> args)
        {
            List<ObjectLog> objectLogs = new List<ObjectLog>();

            foreach (object arg in args)
            {
                IObjectConverter objectConverter = ObjectConvertersFactory.ConverterByHandledType(arg.GetType());

                if (objectConverter != null)
                {
                    objectLogs.Add(objectConverter.ConvertToObjectLog(arg));
                }
            }

            return objectLogs.ToArray();
        }

        #endregion
    }
}