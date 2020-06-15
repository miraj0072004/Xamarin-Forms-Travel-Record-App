using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XamarinLearning.ViewModels.Commands
{
    public class AddPostCommand:ICommand
    {
        private readonly NewTravelPageVM _newTravelPageVm;

        public AddPostCommand(NewTravelPageVM newTravelPageVM)
        {
            _newTravelPageVm = newTravelPageVM;
        }
        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            var experience = (string) parameter;

            if (string.IsNullOrEmpty(experience))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
             _newTravelPageVm.AddPost();
        }

        public event EventHandler CanExecuteChanged;
    }
}
