using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using XamarinLearning.Annotations;
using XamarinLearning.Models;
using XamarinLearning.ViewModels.Commands;

namespace XamarinLearning.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
        public LoginCommand LoginCommand { get; set; }
        public GoToRegisterCommand GoToRegisterCommand { get; set; }

        private Myuser _user;

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;

                User = new Myuser
                {
                    Email = Email,
                    Password = Password
                };
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                User = new Myuser
                {
                    Email = Email,
                    Password = Password
                };
                OnPropertyChanged();
            }
        }



        public Myuser User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public LoginVM()
        {
            LoginCommand = new LoginCommand(this);
            GoToRegisterCommand = new GoToRegisterCommand(this);
        }

        public async void Login()
        {
            var canLogin = await Myuser.Login(Email, Password);

            if (canLogin)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Try again", "Ok");
            }
        }

        public async void GoToRegister()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
