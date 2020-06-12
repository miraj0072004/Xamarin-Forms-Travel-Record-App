using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLearning.ViewModels;

namespace XamarinLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            HomeVM homeVm = new HomeVM();
            BindingContext = homeVm;
        }

        //private void MenuItem_OnClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new NewTravelPage());
        //}
    }
}