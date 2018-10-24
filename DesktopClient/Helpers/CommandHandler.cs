using System;
using System.Windows.Input;

namespace DesktopClient
{
    class CommandHandler : ICommand
    {
        private readonly Action<object> _action;

        private readonly Func<object, bool> _canExecute;
        //private readonly bool _canExecute;
        public CommandHandler(Action<object> action) : this(action, null)
        {
            
        }
        public CommandHandler(Action<object> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
