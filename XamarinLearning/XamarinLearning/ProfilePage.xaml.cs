using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Models;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = conn.Table<Post>().ToList();
                PostCountLabel.Text = postTable.Count.ToString();

                var categories = postTable.OrderBy(x => x.CategoryName)
                                                     .Select(x => x.CategoryName)
                                                     .Distinct()
                                                     .ToList();
                //Another way of writing the above query
                //var categories = (from p in postTable
                //    orderby p.CategoryName
                //    select p.CategoryName).Distinct().ToList();


                Dictionary<string,int> categoriesCount = new Dictionary<string, int>();

                foreach (var category in categories)
                {
                    var count = postTable.Where(x => x.CategoryName == category).ToList().Count;

                    //Another way of writing the above query
                    //var count = (from p in postTable
                    //    where p.CategoryName == category
                    //    select p).ToList().Count;
                    var categoryToAssign = category ?? "Empty";
                    categoriesCount.Add(categoryToAssign, count);
                }

                CategoriesListView.ItemsSource = categoriesCount;
            }
        }
    }
}