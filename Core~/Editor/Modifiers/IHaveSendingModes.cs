using DebuggingTools.Runtime.Data;

namespace DebuggingTools.Editor.Modifiers
{
    public interface IHaveSendingModes
    {
        #region Public Properties

        LogSendingMode AnalyticsLogSendingMode { get; }
        LogSendingMode FileLogSendingMode { get; }

        #endregion
    }
}