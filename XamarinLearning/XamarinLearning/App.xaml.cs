using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Models;

namespace XamarinLearning
{
    public partial class App : Application
    {

        public static string DatabaseLocation = string.Empty;
        public static MobileServiceClient MobileService =
            new MobileServiceClient("https://travelrecordmirajapp.azurewebsites.net");

        public static IMobileServiceSyncTable<Post> postTable;

        public static Myuser user = new Myuser();
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            DatabaseLocation = databaseLocation;

            var store = new MobileServiceSQLiteStore(databaseLocation);
            store.DefineTable<Post>();

            MobileService.SyncContext.InitializeAsync(store);

            postTable = MobileService.GetSyncTable<Post>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
