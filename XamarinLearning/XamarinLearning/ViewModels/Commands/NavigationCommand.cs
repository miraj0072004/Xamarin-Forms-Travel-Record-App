using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XamarinLearning.ViewModels.Commands
{
    public class NavigationCommand: ICommand
    {
        private readonly HomeVM _homeVm;

        public NavigationCommand(HomeVM homeVm)
        {
            _homeVm = homeVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _homeVm.Navigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}
