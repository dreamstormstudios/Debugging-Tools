namespace DebuggingTools.Runtime.Data
{
    /// <summary>
    ///     Struct responsible for:
    ///     - wrapping information about object
    /// </summary>
    public struct ObjectLog
    {
        #region Public Properties

        /// <summary>
        ///     Description of information about object.
        /// </summary>
        public string Description { get; }
        /// <summary>
        ///     Information about object.
        /// </summary>
        public string Content { get; }

        #endregion

        #region Constructors

        public ObjectLog(string description, string content)
        {
            Description = description;
            Content = content;
        }

        #endregion
    }
}