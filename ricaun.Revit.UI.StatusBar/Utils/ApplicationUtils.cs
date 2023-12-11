namespace ricaun.Revit.UI.StatusBar
{
    /// <summary>
    /// ApplicationUtils
    /// </summary>
    public static class ApplicationUtils
    {
        /// <summary>
        /// DoEvents
        /// </summary>
        public static void DoEvents()
        {
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// SetCursorWait
        /// </summary>
        public static void SetCursorWait()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        /// <summary>
        /// SetCursorDefault
        /// </summary>
        public static void SetCursorDefault()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }
    }
}
