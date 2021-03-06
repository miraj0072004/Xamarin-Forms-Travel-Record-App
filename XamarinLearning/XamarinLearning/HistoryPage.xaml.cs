﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.Models;
using XamarinLearning.ViewModels;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryVm historyVm;
        public HistoryPage()
        {
            
            InitializeComponent();
            historyVm = new HistoryVm();
            BindingContext = historyVm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    var posts = conn.Table<Post>().ToList();
            //    PostListView.ItemsSource = posts;
            //}

            //var posts = await Post.Read();

            historyVm.UpdatePosts();



        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            var post = (Post) ((MenuItem) sender).CommandParameter;
            historyVm.DeletePost(post);

            historyVm.UpdatePosts();
        }

        private async void PostListView_OnRefreshing(object sender, EventArgs e)
        {
            await historyVm.UpdatePosts();
            PostListView.IsRefreshing = false;
        }
    }
}