using eKitchen.Views;
using Xamarin.Forms;

namespace eKitchen
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new WelcomePage());

            InitializeComponent();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
