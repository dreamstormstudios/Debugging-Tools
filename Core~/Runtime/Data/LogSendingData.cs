using System;
using DebuggingTools.Data;
using UnityEngine;

namespace DebuggingTools.Runtime.Data
{
    [Serializable]
    public struct LogSendingData
    {
        #region Serialized Fields

        [SerializeField]
        private LogType logType;
        [SerializeField]
        private LogCategory logCategory;

        #endregion

        #region Public Properties

        public LogType LogType => logType;
        public LogCategory LogCategory => logCategory;

        #endregion

        #region Constructors

        public LogSendingData(LogType logType, LogCategory logCategory)
        {
            this.logType = logType;
            this.logCategory = logCategory;
        }

        #endregion
    }
}