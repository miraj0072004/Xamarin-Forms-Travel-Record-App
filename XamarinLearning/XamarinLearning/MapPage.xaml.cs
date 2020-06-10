using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Models;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;

            await locator.StartListeningAsync(new TimeSpan(0), 100);

            var position = await locator.GetPositionAsync();

            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude );
            var span = new Xamarin.Forms.Maps.MapSpan(center,2 ,2);
            locationsMap.MoveToRegion(span);

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    var posts = conn.Table<Post>().ToList();

            //    DisplayInMaps(posts);
            //}

            var posts = await Post.Read();
            DisplayInMaps(posts);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMaps(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Xamarin.Forms.Maps.Pin
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                {

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}