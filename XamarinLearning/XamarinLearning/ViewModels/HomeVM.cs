using System;
using System.Collections.Generic;
using System.Text;
using XamarinLearning.ViewModels.Commands;

namespace XamarinLearning.ViewModels
{
    public class HomeVM
    {
        public NavigationCommand navCommand { get; set; }

        public HomeVM()
        {
            navCommand = new NavigationCommand(this);
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}
