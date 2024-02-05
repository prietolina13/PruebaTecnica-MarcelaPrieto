using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaTecnica_MarcelaPrieto.ViewModel
{
    public class ViewModelCommand : ICommand
    {
        //Definición de campos Action para ejecutar comandos y Predicate para determinar si una acción se puede o no ejecutar.
        private readonly Action<object> _execute;
        private readonly Predicate<object> _predicate;

        //Definición de constructor
        public ViewModelCommand(Action<object> execute)
        {
            _execute = execute;
            _predicate = null;
        }

        public ViewModelCommand(Action<object> execute, Predicate<object> predicate)
        {
            _execute = execute;
            _predicate = predicate;
        }

        //Implementación de Interfaz
        //Event
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methods
        public bool CanExecute(object? parameter)
        {
            return _predicate == null ? true : _predicate(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
