using System;
using System.Windows.Input;

namespace ricaun.Revit.UI.StatusBar.Utils
{
    internal class RelayCommand : ICommand
    {
        private Action action;
#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
        public RelayCommand(Action action)
        {
            this.action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
