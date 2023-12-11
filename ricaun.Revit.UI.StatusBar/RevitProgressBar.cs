using ricaun.Revit.UI.StatusBar.Controls;
using ricaun.Revit.UI.StatusBar.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ricaun.Revit.UI.StatusBar
{
    /// <summary>
    /// RevitProgressBar
    /// </summary>
    public class RevitProgressBar : IDisposable
    {
        private readonly Stopwatch stopwatch;
        private readonly ProgressBarStackPanel progressBarStackPanel;

        /// <summary>
        /// RevitProgressBar
        /// </summary>
        /// <param name="hasCancelButton"></param>
        public RevitProgressBar(bool hasCancelButton = false)
        {
            stopwatch = Stopwatch.StartNew();
            progressBarStackPanel = new ProgressBarStackPanel(hasCancelButton);
            this.ForceToRefresh = StatusBarController.IsVisible;
        }
        /// <summary>
        /// Run
        /// </summary>
        /// <param name="currentOperation"></param>
        /// <param name="count"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public RevitProgressBar Run(string currentOperation, int count, Action<int> action)
        {
            return SetCurrentOperation(currentOperation).Run(count, action);
        }
        /// <summary>
        /// Run
        /// </summary>
        /// <param name="count"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public RevitProgressBar Run(int count, Action<int> action)
        {
            return Run(Enumerable.Range(0, count), action);
        }
        /// <summary>
        /// Run
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentOperation"></param>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public RevitProgressBar Run<T>(string currentOperation, IEnumerable<T> collection, Action<T> action)
        {
            return SetCurrentOperation(currentOperation).Run(collection, action);
        }

        /// <summary>
        /// Run
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public RevitProgressBar Run<T>(IEnumerable<T> collection, Action<T> action)
        {
            progressBarStackPanel.Data.CurrentValue = 0;
            progressBarStackPanel.Data.MinimumValue = 0;
            progressBarStackPanel.Data.MaximumValue = collection.Count();
            foreach (var item in collection)
            {
                var current = progressBarStackPanel.Data.CurrentValue;
                action?.Invoke(item);
                progressBarStackPanel.Data.CurrentValue = current + 1;
                if (RefreshStopwatchBackground().IsCancelling())
                    break;
            }
            return this;
        }

        /// <summary>
        /// SetCurrentOperation
        /// </summary>
        /// <param name="currentOperation"></param>
        /// <returns></returns>
        public RevitProgressBar SetCurrentOperation(string currentOperation)
        {
            progressBarStackPanel.Data.CurrentOperation = currentOperation;
            return this;
        }

        /// <summary>
        /// SetCurrentValue
        /// </summary>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        public RevitProgressBar SetCurrentValue(double currentValue)
        {
            progressBarStackPanel.Data.CurrentValue = currentValue;
            return this;
        }

        /// <summary>
        /// SetMinimumValue
        /// </summary>
        /// <param name="minimumValue"></param>
        /// <returns></returns>
        public RevitProgressBar SetMinimumValue(double minimumValue)
        {
            progressBarStackPanel.Data.MinimumValue = minimumValue;
            return this;
        }

        /// <summary>
        /// SetMaximumValue
        /// </summary>
        /// <param name="maximumValue"></param>
        /// <returns></returns>
        public RevitProgressBar SetMaximumValue(double maximumValue)
        {
            progressBarStackPanel.Data.MaximumValue = maximumValue;
            return this;
        }

        /// <summary>
        /// SetIsIndeterminate
        /// </summary>
        /// <param name="isIndeterminate"></param>
        /// <returns></returns>
        public RevitProgressBar SetIsIndeterminate(bool isIndeterminate)
        {
            progressBarStackPanel.Data.IsIndeterminate = isIndeterminate;
            return this;
        }

        /// <summary>
        /// SetHasCancelButton
        /// </summary>
        /// <param name="hasCancelButton"></param>
        /// <returns></returns>
        public RevitProgressBar SetHasCancelButton(bool hasCancelButton)
        {
            progressBarStackPanel.Data.HasCancelButton = hasCancelButton;
            return this;
        }

        /// <summary>
        /// Increment
        /// </summary>
        /// <param name="incrementCurrentValue"></param>
        /// <returns></returns>
        public RevitProgressBar Increment(int incrementCurrentValue = 1)
        {
            progressBarStackPanel.Data.CurrentValue += incrementCurrentValue;
            RefreshStopwatchBackground();
            return this;
        }

        private bool cancelPressed { get; set; } = false;
        /// <summary>
        /// IsCancelling
        /// </summary>
        /// <returns></returns>
        public bool IsCancelling()
        {
            if (progressBarStackPanel.Data.CommandCancel is null)
            {
                progressBarStackPanel.Data.CommandCancel = new RelayCommand(Cancel);
            }
            return cancelPressed;
        }
        /// <summary>
        /// Cancel
        /// </summary>
        public void Cancel()
        {
            cancelPressed = true;
        }

        private bool ForceToRefresh;
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            StatusBarController.Hide();
            stopwatch.Stop();

            if (ForceToRefresh)
            {
                RefreshBackground(true);
            }
        }

        /// <summary>
        /// Refresh Milliseconds (default: 50)
        /// </summary>
        public int RefreshMilliseconds { get; set; } = 50;
        /// <summary>
        /// Initialize Milliseconds (default: 250)
        /// </summary>
        public int InitializeMilliseconds { get; set; } = 250;

        private RevitProgressBar RefreshStopwatchBackground()
        {
            if (InitializeMilliseconds > 0 && stopwatch.ElapsedMilliseconds < InitializeMilliseconds)
            {
                return this;
            }
            InitializeMilliseconds = 0;
            if (stopwatch.ElapsedMilliseconds > RefreshMilliseconds)
            {
                StatusBarController.Show(progressBarStackPanel);
                RefreshBackground();
                stopwatch.Restart();
            }
            return this;
        }

        private void RefreshBackground(bool disable = false)
        {
            //if (!disable)
            //    RevitRibbonController.Disable();

            ApplicationUtils.DoEvents();
            //ApplicationUtils.SetCursorWait();
            //progressBarStackPanel.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Render);
            if (disable)
            {
                ApplicationUtils.SetCursorDefault();
                RevitRibbonController.Enable();
            }
            else
            {
                ApplicationUtils.SetCursorWait();
                RevitRibbonController.Disable();
            }
        }
    }
}
