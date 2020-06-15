using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XamarinLearning.ViewModels.Commands
{
    public class GoToRegisterCommand: ICommand
    {
        private readonly LoginVM _loginVm;

        public GoToRegisterCommand(LoginVM loginVM)
        {
            _loginVm = loginVM;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _loginVm.GoToRegister();
        }

        public event EventHandler CanExecuteChanged;
    }
}
