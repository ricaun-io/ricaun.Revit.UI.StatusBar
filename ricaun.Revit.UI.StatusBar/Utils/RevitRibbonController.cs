using UIFramework;

namespace ricaun.Revit.UI.StatusBar.Utils
{
    /// <summary>
    /// RevitRibbonController
    /// </summary>
    public class RevitRibbonController
    {
        /// <summary>
        /// RibbonControl
        /// </summary>
        public static RevitRibbonControl RibbonControl => UIFramework.RevitRibbonControl.RibbonControl;

        /// <summary>
        /// Enable
        /// </summary>
        public static void Enable() => RibbonControl.IsEnabled = true;
        /// <summary>
        /// Disable
        /// </summary>
        public static void Disable() => RibbonControl.IsEnabled = false;
    }

}
