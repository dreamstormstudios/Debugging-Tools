using System.Collections.Generic;
using DebuggingTools.Runtime.Data;

namespace DebuggingTools.Editor.Modifiers
{
    public interface IHaveSendingConfiguration
    {
        #region Public Properties

        string Category { get; }

        #endregion

        #region Public Methods

        IEnumerable<LogType> GetLogTypesEnabledInEditor();
        IEnumerable<LogType> GetLogTypesEnabledInBuild();

        #endregion
    }
}