using System;
using DebuggingTools.Runtime.Data;

namespace DebuggingTools.Runtime.Interfaces
{
    /// <summary>
    ///     Interface responsible for:
    ///     - converting object of given type to ObjectLog
    /// </summary>
    public interface IObjectConverter
    {
        #region Public Properties

        /// <summary>
        ///     Type of object that will be send to ConvertToObjectLog method.
        /// </summary>
        Type HandledType { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Method responsible for converting object to ObjectLog.
        /// </summary>
        /// <param name="convertedObject">This object is guaranteed to be the same type as defined in HandledType property.</param>
        /// <returns>Message and its' description about object wrapped in ObjectLog.</returns>
        ObjectLog ConvertToObjectLog(object convertedObject);

        #endregion
    }
}