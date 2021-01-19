using System.Collections.Generic;

namespace DebuggingTools.Runtime.Data
{
    public class Log
    {
        #region Private Fields

        private string description;
        private ObjectLog[] objectLogs;

        #endregion

        #region Public Properties

        public LogType Type { get; }
        public string Description => $"{Type.ToString()}: {description}";

        #endregion

        #region Constructors

        public Log(string description, LogType type, ObjectLog[] objectLogs)
        {
            Type = type;
            this.description = description;
            this.objectLogs = objectLogs;
        }

        #endregion

        #region Public Methods

        public string ConvertToString()
        {
            if (objectLogs.Length == 0)
            {
                return string.Empty;
            }

            int lastLogIndex = objectLogs.Length - 1;
            string outputValue = $"{Description}\n";

            for (int i = 0; i < lastLogIndex; i++)
            {
                outputValue += $"{objectLogs[i].Description}: {objectLogs[i].Content}\n";
            }

            outputValue += $"{objectLogs[lastLogIndex].Description}: {objectLogs[lastLogIndex].Content}";

            return outputValue;
        }

        public Dictionary<string, object> ConvertToDictionary()
        {
            Dictionary<string, object> outputValue = new Dictionary<string, object>();

            foreach (ObjectLog messageData in objectLogs)
            {
                int messageIndex = 0;

                while (outputValue.ContainsKey($"{messageData.Description}({messageIndex})"))
                {
                    messageIndex++;
                }

                outputValue.Add($"{messageData.Description}({messageIndex})", messageData.Content);
            }

            return outputValue;
        }

        #endregion
    }
}