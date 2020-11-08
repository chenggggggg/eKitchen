using eKitchen.Views;
using Xamarin.Forms;
using eKitchen.Viewmodels;
using Xamarin.Essentials;
using eKitchen.Services;
using eKitchen.Models;

namespace eKitchen
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new WelcomePage());

            BindingContext = new LoginViewmodel(MainPage.Navigation);

            InitializeComponent();
        }

        protected override async void OnStart()
        {
            await (BindingContext as LoginViewmodel).AutoLogin();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
