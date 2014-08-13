using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PixelPicker.Helpers
{
    /// <summary>
    /// A generic Relay Command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action<T> methodToExecute;
        private Func<T, bool> canExecute;
        public RelayCommand(Action<T> methodToExecute, Func<T, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecute = canExecuteEvaluator;
        }
        public RelayCommand(Action<T> methodToExecute)
            : this(methodToExecute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecute.Invoke((T)parameter);
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke((T)parameter);
        }


    }
}
