using System.Windows.Forms;

namespace OCRComparer.Utils
{
    internal class ErrorHandler
    {
        public static void ShowErrorMessageBox(string errorMessage, string title = null)
        {
            MessageBox.Show(errorMessage, "Error" + title != null ? ": " + title : string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowWarningMessageBox(string errorMessage, string title = null)
        {
            MessageBox.Show(errorMessage, "Warning" + title != null ? ": " + title : string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowInfoMessageBox(string errorMessage, string title = null)
        {
            MessageBox.Show(errorMessage, "Info" + title != null ? ": " + title : string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
