using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;
using XamarinLearning.Annotations;
using XamarinLearning.Models;
using XamarinLearning.ViewModels.Commands;


namespace XamarinLearning.ViewModels
{
    public class NewTravelPageVM : INotifyPropertyChanged
    {
        private Venue _selectedItem;

        public Venue SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private List<Venue> _venues;

        public List<Venue> Venues
        {
            get { return _venues; }
            set
            {
                _venues = value;
                OnPropertyChanged();
            }
        }

        private string _experience;

        public string Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                OnPropertyChanged();
            }
        }

        public AddPostCommand AddPostCommand { get; set; }

        public NewTravelPageVM()
        {
           GetVenues();
           AddPostCommand = new AddPostCommand(this);
        }

        async void  GetVenues()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            Venues = await Venue.GetVenues(position.Latitude, position.Longitude);
        }

        public async void AddPost()
        {
            var post = new Post();
            try
            {
                var selectedVenue = SelectedItem;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                post.Experience = Experience;

                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = selectedVenue.location.address;
                post.Distance = selectedVenue.location.distance;
                post.Latitude = selectedVenue.location.lat;
                post.Longitude = selectedVenue.location.lng;
                post.VenueName = selectedVenue.name;
                post.UserId = App.user.Id;



                //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                //{
                //    conn.CreateTable<Post>();
                //    int rows = conn.Insert(post);

                //    if (rows > 0)
                //    {
                //        DisplayAlert("Success", "Experience successfully inserted", "Ok");
                //    }
                //    else
                //    {
                //        DisplayAlert("Failure", "Experience inserted failed", "Ok");
                //    }
                //}

                //await App.MobileService.GetTable<Post>().InsertAsync(post);
                Post.InsertPost(post);
                await Application.Current.MainPage.DisplayAlert("Success", "Experience successfully inserted", "Ok");

            }
            catch (NullReferenceException nre)
            {
                await Application.Current.MainPage.DisplayAlert("Failure", "Experience inserted failed", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failure", "Experience inserted failed", "Ok");
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
