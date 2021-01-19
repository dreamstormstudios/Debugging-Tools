using System.Text;

namespace DebuggingTools.Editor.Modifiers
{
    public class Writer
    {
        #region Private Fields

        private int indentLevel;
        private StringBuilder buffer;

        #endregion

        #region Public Properties

        public string Content => buffer.ToString();

        #endregion

        #region Constructors

        public Writer()
        {
            buffer = new StringBuilder();
        }

        #endregion

        #region Public Methods

        public void BeginBlock()
        {
            WriteIndent();
            buffer.Append("{\n");
            indentLevel++;
        }

        public void EndBlock()
        {
            indentLevel--;
            WriteIndent();
            buffer.Append("}\n");
        }

        public void WriteLine(string textToWrite)
        {
            WriteIndent();
            buffer.Append(textToWrite);
            buffer.Append('\n');
        }

        #endregion

        #region Private Methods

        private void WriteIndent()
        {
            for (int i = 0; i < indentLevel; i++)
            {
                buffer.Append('\t');
            }
        }

        #endregion
    }
}