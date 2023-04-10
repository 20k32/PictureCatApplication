using System.Windows.Input;
using System;
using System.Threading.Tasks;

namespace PictureCat
{
    public class MyRoutedCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object>? _canExecute;

        public MyRoutedCommand(Action<object> execute) : this(execute, null)
        { }

        public MyRoutedCommand(Action<object> execute, Predicate<object>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter!);
        }
    }

    public class MyRoutedCommandAsync : ICommand
    {
        private Func<object, Task<object>> _execute;
        private Predicate<object>? _canExecute;

        public MyRoutedCommandAsync(Func<object, Task<object>> execute) : this(execute, null)
        { }

        public MyRoutedCommandAsync(Func<object, Task<object>> execute, Predicate<object>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke(parameter!);
        }

        public async void Execute(object? parameter)
        {
            await _execute.Invoke(parameter!);
        }
    }
}
