using System;

namespace ricaun.Revit.UI.StatusBar.Utils
{
    /// <summary>
    /// BalloonUtils
    /// </summary>
    public static class BalloonUtils
    {
        /// <summary>
        /// Show
        /// </summary>
        /// <param name="text"></param>
        /// <param name="category"></param>
        /// <param name="uri"></param>
        /// <param name="favorite"></param>
        /// <remarks>https://thebuildingcoder.typepad.com/blog/2014/03/using-balloon-tips-in-revit.html</remarks>
        public static void Show(string text, string category = null, string uri = null, bool favorite = false)
        {
            var ri = new Autodesk.Internal.InfoCenter.ResultItem();

            if (!string.IsNullOrWhiteSpace(category))
                ri.Category = category;

            ri.Title = text;
            ri.IsFavorite = favorite;

            try { ri.Uri = new Uri(uri); } catch { }
            Autodesk.Windows.ComponentManager.InfoCenterPaletteManager.ShowBalloon(ri);
        }
    }
}