using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XamarinLearning.Models;

namespace XamarinLearning.ViewModels.Commands
{
    public class LoginCommand: ICommand
    {
        private readonly LoginVM _loginVm;

        public LoginCommand(LoginVM loginVm)
        {
            _loginVm = loginVm;
        }
        public bool CanExecute(object parameter)
        {
            var user = (Myuser) parameter;

            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            _loginVm.Login();
        }

        public event EventHandler CanExecuteChanged;
    }
}
