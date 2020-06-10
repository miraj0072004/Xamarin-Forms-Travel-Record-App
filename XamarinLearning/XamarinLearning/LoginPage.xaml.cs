using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinLearning.Models;

namespace XamarinLearning
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var assembly = typeof(LoginPage);
            iconImage.Source = ImageSource.FromResource("XamarinLearning.Assets.Images.plane.png", assembly);
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var canLogin = await Myuser.Login(emailEntry.Text, passwordEntry.Text);

            if (canLogin)
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Try again", "Ok");
            }

        }

        private void RegisterUserButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
