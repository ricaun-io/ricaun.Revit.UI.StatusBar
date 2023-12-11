using ricaun.Revit.UI.StatusBar.Controls;
using ricaun.Revit.UI.StatusBar.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ricaun.Revit.UI.StatusBar
{
    public class RevitProgressBar : IDisposable
    {
        private readonly Stopwatch stopwatch;
        private readonly ProgressBarStackPanel progressBarStackPanel;

        public RevitProgressBar(bool hasCancelButton = false)
        {
            stopwatch = Stopwatch.StartNew();
            progressBarStackPanel = new ProgressBarStackPanel(hasCancelButton);
            this.ForceToRefresh = StatusBarController.IsVisible;
        }
        public RevitProgressBar Run(string currentOperation, int count, Action<int> action)
        {
            return SetCurrentOperation(currentOperation).Run(count, action);
        }
        public RevitProgressBar Run(int count, Action<int> action)
        {
            return Run(Enumerable.Range(0, count), action);
        }
        public RevitProgressBar Run<T>(string currentOperation, IEnumerable<T> collection, Action<T> action)
        {
            return SetCurrentOperation(currentOperation).Run(collection, action);
        }
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

        public RevitProgressBar SetCurrentOperation(string currentOperation)
        {
            progressBarStackPanel.Data.CurrentOperation = currentOperation;
            return this;
        }

        public RevitProgressBar SetCurrentValue(double currentValue)
        {
            progressBarStackPanel.Data.CurrentValue = currentValue;
            return this;
        }

        public RevitProgressBar SetMinimumValue(double minimumValue)
        {
            progressBarStackPanel.Data.MinimumValue = minimumValue;
            return this;
        }

        public RevitProgressBar SetMaximumValue(double maximumValue)
        {
            progressBarStackPanel.Data.MaximumValue = maximumValue;
            return this;
        }

        public RevitProgressBar SetIsIndeterminate(bool isIndeterminate)
        {
            progressBarStackPanel.Data.IsIndeterminate = isIndeterminate;
            return this;
        }

        public RevitProgressBar SetHasCancelButton(bool hasCancelButton)
        {
            progressBarStackPanel.Data.HasCancelButton = hasCancelButton;
            return this;
        }

        public RevitProgressBar Increment(int currentValuePlus = 1)
        {
            progressBarStackPanel.Data.CurrentValue += currentValuePlus;
            RefreshStopwatchBackground();
            return this;
        }

        private bool cancelPressed { get; set; } = false;
        public bool IsCancelling()
        {
            if (progressBarStackPanel.Data.CommandCancel is null)
            {
                progressBarStackPanel.Data.CommandCancel = new RelayCommand(Cancel);
            }
            return cancelPressed;
        }
        public void Cancel()
        {
            cancelPressed = true;
        }

        private bool ForceToRefresh;
        public void Dispose()
        {
            StatusBarController.Hide();
            stopwatch.Stop();

            if (ForceToRefresh)
                RefreshBackground();
        }

        /// <summary>
        /// Refresh Milliseconds (default: 50)
        /// </summary>
        public int RefreshMilliseconds { get; set; } = 50;

        private RevitProgressBar RefreshStopwatchBackground()
        {
            if (stopwatch.ElapsedMilliseconds > RefreshMilliseconds)
            {
                StatusBarController.Show(progressBarStackPanel);
                RefreshBackground();
                stopwatch.Restart();
            }
            return this;
        }

        private void RefreshBackground()
        {
            progressBarStackPanel.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }
    }
}
