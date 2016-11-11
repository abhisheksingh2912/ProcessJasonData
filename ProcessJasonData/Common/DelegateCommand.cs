using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessJasonData.Common
{
    public class DelegateCommand : ICommand
    {
        Action _execute;
        Func<bool> _canexecute;
        public DelegateCommand(Action execute, Func<bool> canexecute)
        {
            _execute = execute;
            _canexecute = canexecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
