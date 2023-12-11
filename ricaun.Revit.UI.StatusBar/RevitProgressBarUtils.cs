using System;
using System.Collections.Generic;

namespace ricaun.Revit.UI.StatusBar
{
    public static class RevitProgressBarUtils
    {
        public static void Run(string currentOperation, int count, Action<int> action)
        {
            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.Run(currentOperation, count, action);
            }
        }

        public static void Run<T>(string currentOperation, IEnumerable<T> collection, Action<T> action)
        {
            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.Run(currentOperation, collection, action);
            }
        }
    }
}
