using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XamarinLearning.ViewModels.Commands
{
    public class RegisterCommand: ICommand
    {
        private readonly RegisterVM _registerVm;

        public RegisterCommand(RegisterVM registerVm)
        {
            _registerVm = registerVm;
        }
        public bool CanExecute(object parameter)
        {
            bool anyInput;
            if (parameter == null)
            {
                anyInput = false;
            }
            else
            {
                anyInput = (bool)parameter;
            }
            

            if (anyInput)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            _registerVm.Register();
        }

        public event EventHandler CanExecuteChanged;
    }
}
