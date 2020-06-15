using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinLearning.Models;
using XamarinLearning.ViewModels;

namespace XamarinLearning
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginVM LoginVm { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            var assembly = typeof(LoginPage);
            iconImage.Source = ImageSource.FromResource("XamarinLearning.Assets.Images.plane.png", assembly);

            LoginVm = new LoginVM {Email = "sample@jdf.com", Password = "sample"};
            this.BindingContext = LoginVm;
        }



        private void RegisterUserButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
