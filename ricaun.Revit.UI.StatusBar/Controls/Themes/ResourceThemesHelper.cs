using ricaun.Revit.UI.StatusBar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ricaun.Revit.UI.StatusBar.Controls.Themes
{
    /// <summary>
    /// ResourceThemesHelper
    /// </summary>
    public static class ResourceThemesHelper
    {
        /// <summary>
        /// AddResourceThemes for ProgressBar
        /// </summary>
        /// <param name="window"></param>
        /// <param name="defaultThemeName">default, light, dark</param>
        public static void AddResourceThemes(this FrameworkElement window, string defaultThemeName = null)
        {
            try
            {
                if (defaultThemeName is null)
                    defaultThemeName = GetRevitThemeName();

                var resourceNames = GetResourceThemes()
                    .OrderBy(e => e.Contains(defaultThemeName));

                foreach (var resourceName in resourceNames)
                {
                    try
                    {
                        var resource = new ResourceDictionary
                        {
                            Source = ResourceHelper.GetAbsoluteUri(resourceName)
                        };

                        var similarResource = window.Resources.MergedDictionaries.FirstOrDefault(e => e.SimilarSource(resource));
                        if (similarResource is not null)
                            window.Resources.MergedDictionaries.Remove(similarResource);

                        window.Resources.MergedDictionaries.Add(resource);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            catch { }
        }

        private static int RevitVersion { get; } = typeof(Autodesk.Revit.UI.UIThemeManager).Assembly.GetName().Version.Major;

        private static string GetRevitThemeName()
        {
            var revitThemeName = "default";
            try
            {
                if (RevitVersion >= 24)
                {
                    revitThemeName = "light";
                    var currentTheme = Autodesk.Revit.UI.UIThemeManager.CurrentTheme;
                    if (currentTheme == Autodesk.Revit.UI.UITheme.Dark)
                    {
                        revitThemeName = "dark";
                    }
                }

            }
            catch { }
            return revitThemeName;
        }

        private static IEnumerable<string> GetResourceThemes()
        {
            return ResourceHelper.GetResourceNames()
                .Where(e => e.Contains("/themes/"))
                .Where(e => e.EndsWith(".xaml"));
        }
    }
}