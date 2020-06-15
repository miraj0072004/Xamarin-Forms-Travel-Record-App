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
    public class RegisterVM : INotifyPropertyChanged
    {
        public RegisterCommand RegisterCommand { get; set; }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;

                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
                {
                    AnyInput = true;
                }
                else
                {
                    AnyInput = false;
                }

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
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
                {
                    AnyInput = true;
                }
                else
                {
                    AnyInput = false;
                }

                OnPropertyChanged();
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
                {
                    AnyInput = true;
                }
                else
                {
                    AnyInput = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _anyInput;

        public bool AnyInput
        {
            get { return _anyInput; }
            set
            {
                _anyInput = value;
                OnPropertyChanged();
            }
        } 




        public RegisterVM()
        {
            AnyInput = false;
            RegisterCommand = new RegisterCommand(this);
            
        }

        public async void Register()
        {
            if (Password == ConfirmPassword)
            {
                //We register the user
                //Myuser user = new Myuser
                //{
                //    Email = emailEntry.Text,
                //    Password = passwordEntry.Text
                //};

                Myuser.Register(new Myuser{Email = Email, Password = Password});
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
