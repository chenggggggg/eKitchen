using eKitchen.Viewmodels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eKitchen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //private LoginViewmodel viewmodel = new LoginViewmodel();

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new LoginViewmodel(Navigation);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await (BindingContext as LoginViewmodel).LoginUser();
        }
    }
}