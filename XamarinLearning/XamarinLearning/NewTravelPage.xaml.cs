﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Logic;
using XamarinLearning.Models;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            VenueListView.ItemsSource = venues;
        } 

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = VenueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                Post post = new Post()
                {
                    Experience = ExperienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name,
                    UserId = App.user.Id
                };


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

                await App.MobileService.GetTable<Post>().InsertAsync(post);
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