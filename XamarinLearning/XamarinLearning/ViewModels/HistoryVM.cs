using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinLearning.Models;

namespace XamarinLearning.ViewModels
{
    public class HistoryVm
    {
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public async void UpdatePosts()
        {
            var posts = await Post.Read();

            if (posts != null)
            {
                Posts.Clear();
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
        }
    }
}
