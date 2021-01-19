using System.IO;

namespace DebuggingTools.Runtime
{
    public static class FileExtensions
    {
        #region Public Methods

        public static void AppendToFile(string text, string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(text))
            {
                return;
            }

            CreateDirectoryForFile(filePath);

            if (!File.Exists(filePath))
            {
                File.CreateText(filePath);
            }

            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(text);
            }
        }

        public static void SaveToFile(string text, string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(text))
            {
                return;
            }

            CreateDirectoryForFile(filePath);

            File.WriteAllText(filePath, text);
        }

        public static void CreateDirectoryForFile(string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        #endregion
    }
}