using System;
using System.Windows.Input;

namespace CHAT.Helpers
{
    public class DelegateCommands : ICommand
    {
        public Action<object> execute;
        public Func<object, bool> canExecute;

        public DelegateCommands(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested -= value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
