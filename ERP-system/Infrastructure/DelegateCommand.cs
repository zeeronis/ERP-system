using System;
using System.Windows.Input;

namespace ERP_system.Infrastructure
{
    public class DelegateCommand: ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public DelegateCommand(Action<object> execute,
                               Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
