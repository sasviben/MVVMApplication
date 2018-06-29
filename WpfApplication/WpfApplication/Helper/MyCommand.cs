using System;
using System.Windows.Input;

namespace WpfApplication.Helper
{
    public class MyCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public MyCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        //prvi parametar
        public void Execute(object parameter)
        {
            _execute.Invoke();
        }

        //drugi parametar
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }
        
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}