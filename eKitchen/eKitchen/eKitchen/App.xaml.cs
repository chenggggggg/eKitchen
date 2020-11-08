using eKitchen.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using eKitchen.Viewmodels;
using System.Threading.Tasks;

namespace eKitchen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override async void OnStart()
        {

            //LoginViewmodel loginVM = new LoginViewmodel();
            //await loginVM.AutoLogin();
            //if (loginVM.UserAccount.UserId > 0)
            //{
            //    MainPage = new NavigationPage(new HomePage(new HomeViewmodel()));
            //}

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
