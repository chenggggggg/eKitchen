using eKitchen.Viewmodels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eKitchen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomeViewmodel HomeViewmodel;
        public HomePage(HomeViewmodel viewmodel)
        {
            InitializeComponent();
            HomeViewmodel = viewmodel;
        }


    }
}