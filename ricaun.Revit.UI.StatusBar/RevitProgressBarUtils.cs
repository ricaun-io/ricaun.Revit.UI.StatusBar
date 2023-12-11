using System;
using System.Collections.Generic;

namespace ricaun.Revit.UI.StatusBar
{
    /// <summary>
    /// RevitProgressBarUtils
    /// </summary>
    public static class RevitProgressBarUtils
    {
        /// <summary>
        /// Run using <see cref="RevitProgressBar"/>
        /// </summary>
        /// <param name="currentOperation"></param>
        /// <param name="count"></param>
        /// <param name="action"></param>
        public static void Run(string currentOperation, int count, Action<int> action)
        {
            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.Run(currentOperation, count, action);
            }
        }

        /// <summary>
        /// Run using <see cref="RevitProgressBar"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentOperation"></param>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void Run<T>(string currentOperation, IEnumerable<T> collection, Action<T> action)
        {
            using (var revitProgressBar = new RevitProgressBar())
            {
                revitProgressBar.Run(currentOperation, collection, action);
            }
        }
    }
}
