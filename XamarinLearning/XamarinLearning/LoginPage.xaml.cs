using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        }

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var emailText = emailEntry.Text;
            var passwordText = passwordEntry.Text;

            if (string.IsNullOrEmpty(emailText) || string.IsNullOrEmpty(passwordText))
            {
                
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
