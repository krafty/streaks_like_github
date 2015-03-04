using System;
using System.Windows.Input;

namespace TfsStreak
{
    public class InternalCommand : ICommand
    {
        #region Private Fields

        private Func<object, bool> _canExecuteHandler;
        private Action<object> _executeHandler;

        #endregion Private Fields

        #region Public Constructors

        public InternalCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _executeHandler = execute;
            _canExecuteHandler = canExecute;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Methods

        public bool CanExecute(object parameter)
        {
            return _canExecuteHandler.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler.Invoke(parameter);
        }

        #endregion Public Methods
    }
}