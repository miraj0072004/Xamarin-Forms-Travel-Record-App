using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamarinLearning.Models;

namespace XamarinLearning.ViewModels
{
    public class HistoryVm
    {
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public async Task<bool> UpdatePosts()
        {
            try
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void DeletePost(Post post)
        {
            await Post.DeletePost(post);
        }
    }
}
