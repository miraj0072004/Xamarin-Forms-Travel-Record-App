using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Models;
using XamarinLearning.ViewModels;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private Post post;

        public NewTravelPage()
        {
            InitializeComponent();
            //post = new Post();
            //postStackLayout.BindingContext = post;

            NewTravelPageVM newTravelPageVm = new NewTravelPageVM();
            GetVenues(newTravelPageVm);
            
            BindingContext = newTravelPageVm;

            
        }

        private async void GetVenues(NewTravelPageVM viewModel)
        {
            var status =
                await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    await DisplayAlert("Need Permission","We will have to access your location", "Ok");
                }

                var results = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                if (results == PermissionStatus.Granted)
                {
                    status = results;
                }
            }
            if (status == PermissionStatus.Granted)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                viewModel.Venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            }
            else
            {
                await DisplayAlert("No Permission",
                    "You didn't grant permission to access your location, we cannot proceed", "Ok");
            }
        }

        public List<Venue> Venues { get; set; }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    //var locator = CrossGeolocator.Current;
        //    //var position = await locator.GetPositionAsync();

        //    //var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);

        //    //var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
        //    //VenueListView.ItemsSource = venues;
        //} 

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = VenueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();



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
                await DisplayAlert("Success","Experience successfully inserted", "Ok");

            }
            catch (NullReferenceException nre)
            {
                await DisplayAlert("Failure", "Experience inserted failed", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Experience inserted failed", "Ok");
            }

            
        }
    }
}