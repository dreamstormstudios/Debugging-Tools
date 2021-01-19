using System.IO;

namespace DebuggingTools
{
    public static class FileExtensions
    {
        #region Public Methods

        public static void SaveToFile(string text, string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(text))
            {
                return;
            }

            CreateDirectoryForFile(filePath);

            File.WriteAllText(filePath, text);
        }

        private static void CreateDirectoryForFile(string filePath)
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