using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Models;
using XamarinLearning.ViewModels;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        //private Myuser user;
        public RegisterVM RegisterVm { get; set; }

        public RegisterPage()
        {
            InitializeComponent();

            //user = new Myuser();
            //registerStackLayout.BindingContext = user;

            RegisterVm = new RegisterVM();
            BindingContext = RegisterVm;
        }

        //private async void RegisterButton_OnClicked(object sender, EventArgs e)
        //{
        //    if (passwordEntry.Text == confirmPasswordEntry.Text)
        //    {
        //        //We register the user
        //        //Myuser user = new Myuser
        //        //{
        //        //    Email = emailEntry.Text,
        //        //    Password = passwordEntry.Text
        //        //};

        //        Myuser.Register(user);
        //    }
        //    else
        //    {
        //       await DisplayAlert("Error","Passwords don't match","Ok");
        //    }
        //}
    }
}