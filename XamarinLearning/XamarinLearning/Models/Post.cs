using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using XamarinLearning.Annotations;

namespace XamarinLearning.Models
{
    public class Post: INotifyPropertyChanged
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; } //We use this for Sqlite
        //public string Id { get; set; } //We use this for Azure

        private string id;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }


        //[MaxLength(250)]
        //public string Experience { get; set; }
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


        //public string VenueName { get; set; }

        private string _venueName;

        public string VenueName
        {
            get { return _venueName; }
            set
            {
                _venueName = value;
                OnPropertyChanged();
            }
        }

        //public string CategoryId { get; set; }
        private string _categoryId;

        public string CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        //public string CategoryName { get; set; }
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged();
            }
        }

        //public string Address { get; set; }
        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        //public double Latitude { get; set; }
        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        //public double Longitude { get; set; }
        private double _longitude;

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        //public int Distance { get; set; }
        private int _distance;

        public int Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged();
            }
        }

        //public string UserId { get; set; }
        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _createdAt;

        public DateTimeOffset CreatedAt
        {
            get { return _createdAt; }
            set
            {
                _createdAt = value;
                OnPropertyChanged();
            }
        }



        public static async void InsertPost(Post post)
        {
            //await App.MobileService.GetTable<Post>().InsertAsync(post);
            await App.postTable.InsertAsync(post);
            await App.MobileService.SyncContext.PushAsync();
        }

        public static async Task<bool> DeletePost(Post post)
        {
            try
            {
                //await App.MobileService.GetTable<Post>().DeleteAsync(post);
                await App.postTable.DeleteAsync(post);
                await App.MobileService.SyncContext.PushAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static async Task<List<Post>> Read()
        {
            //var posts = await App.MobileService.GetTable<Post>().
            //    Where(p => p.UserId == App.user.Id).ToListAsync();

            var posts = await App.postTable.
                Where(p => p.UserId == App.user.Id).ToListAsync();

            return posts;
        }

        public static async Task<Dictionary<string, int>> CategoryCount(List<Post> posts)
        {
            

            var categories = posts.OrderBy(x => x.CategoryName)
                .Select(x => x.CategoryName)
                .Distinct()
                .ToList();
            //Another way of writing the above query
            //var categories = (from p in postTable
            //    orderby p.CategoryName
            //    select p.CategoryName).Distinct().ToList();


            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                var count = posts.Where(x => x.CategoryName == category).ToList().Count;

                //Another way of writing the above query
                //var count = (from p in postTable
                //    where p.CategoryName == category
                //    select p).ToList().Count;
                var categoryToAssign = category ?? "Empty";
                categoriesCount.Add(categoryToAssign, count);
            }

            return categoriesCount;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
