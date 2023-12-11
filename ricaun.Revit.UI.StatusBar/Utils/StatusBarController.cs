using ricaun.Revit.UI.StatusBar.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using UIFramework;

namespace ricaun.Revit.UI.StatusBar.Utils
{
    /// <summary>
    /// StatusBarController
    /// </summary>
    public class StatusBarController
    {
        static Grid RootGrid;
        static DialogBarControl InternalControl;
        static ContentPresenter _controlPresenter;
        static StatusBarController()
        {
            RootGrid = MainWindow.getMainWnd().FindChild<Grid>("rootGrid");
            if (RootGrid is null) throw new InvalidOperationException("Cannot find root grid in Revit UI");

            InternalControl = RootGrid.FindChild<DialogBarControl>("statusBar");
            if (InternalControl is null) throw new InvalidOperationException("Cannot find internal control in Revit UI");
        }

        /// <summary>
        /// Default StatusBar is visible
        /// </summary>
        public static bool IsVisible => InternalControl.Visibility == Visibility.Visible;

        /// <summary>
        /// Show
        /// </summary>
        /// <param name="content"></param>
        public static void Show(FrameworkElement content)
        {
            InternalControl.Visibility = Visibility.Hidden;

            if (_controlPresenter is null)
            {
                _controlPresenter = new ContentPresenter();
                RootGrid.Children.Add(_controlPresenter);
            }

            _controlPresenter.Content = content;
            Grid.SetRow(_controlPresenter, Grid.GetRow(InternalControl));
        }

        /// <summary>
        /// Hide
        /// </summary>
        public static void Hide()
        {
            InternalControl.Visibility = Visibility.Visible;
            if (_controlPresenter is null) return;

            RootGrid.Children.Remove(_controlPresenter);
            _controlPresenter = null;
        }
    }

}
