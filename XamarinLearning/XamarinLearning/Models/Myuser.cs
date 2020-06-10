using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XamarinLearning.Annotations;

namespace XamarinLearning.Models
{
    public class Myuser:INotifyPropertyChanged
    {
        //public string Id { get; set; }
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //public string Email { get; set; }
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        //public string Password { get; set; }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        public static async Task<bool> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = (await App.MobileService.GetTable<Myuser>().
                Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

            if (user != null)
            {
                App.user = user;
                if (user.Password == password)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static async void  Register(Myuser user)
        {
            await App.MobileService.GetTable<Myuser>().InsertAsync(user);
        }
    }
}
