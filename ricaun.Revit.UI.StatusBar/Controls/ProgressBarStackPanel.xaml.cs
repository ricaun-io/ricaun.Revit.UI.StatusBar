using Autodesk.Revit.UI;
using ricaun.Revit.UI.StatusBar.Controls.Themes;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ricaun.Revit.UI.StatusBar.Controls
{
    /// <summary>
    /// Interaction logic for ProgressBarStackPanel.xaml
    /// </summary>
    /// https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/progressbar-styles-and-templates?view=netframeworkdesktop-4.8
    public partial class ProgressBarStackPanel
    {
        public ProgressBarData Data { get; } = new ProgressBarData();
        public ProgressBarStackPanel()
        {
            Initialize();
        }
        public ProgressBarStackPanel(bool hasCancelButton)
        {
            Data.HasCancelButton = hasCancelButton;
            Initialize();
        }
        public ProgressBarStackPanel(bool hasCancelButton, double minimumValue, double maximumValue)
        {
            Data.HasCancelButton = hasCancelButton;
            Data.CurrentValue = minimumValue;
            Data.MinimumValue = minimumValue;
            Data.MaximumValue = maximumValue;
            Initialize();
        }

        public ProgressBarStackPanel SetCurrentOperation(string currentOperation)
        {
            Data.CurrentOperation = currentOperation;
            return this;
        }

        private void Initialize()
        {
            DataContext = Data;
            InitializeComponent();
            this.AddResourceThemes();
        }

        [PropertyChanged.AddINotifyPropertyChangedInterface]
        public class ProgressBarData
        {
            public bool IsIndeterminate { get; set; } = false;
            public string CurrentOperation { get; set; } = "Loading";
            public double CurrentValue { get; set; } = 0;
            public double MinimumValue { get; set; } = 0;
            public double MaximumValue { get; set; } = 100;
            public double DisplayValue => ((int)MaximumValue == (int)MinimumValue) ? 100 : 100.0 * (CurrentValue - MinimumValue) / (MaximumValue - MinimumValue);
            public ICommand CommandCancel { get; set; }
            public bool HasCancelButton { get; set; } = false;
        }
    }
}
