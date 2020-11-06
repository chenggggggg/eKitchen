using eKitchen.Models;
using eKitchen.Services;
using eKitchen.Viewmodels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eKitchen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private HttpClient client = new HttpClient();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text.Length > 0 && passwordEntry.Text.Length > 0)
            {
                User user = new User(usernameEntry.Text, passwordEntry.Text);
                
                //Convert object to json and PostAsync to web API. 
                string data = JsonConvert.SerializeObject(user);
                var json = new StringContent(data, Encoding.UTF8, "application/json");
                var responseString = await HttpClientService.Instance.HttpClient.PostAsync("https://10.0.2.2:44342/api/user/login", json);

                //Deserialize json response to object.
                var result = await responseString.Content.ReadAsStringAsync();
                User resultUser = JsonConvert.DeserializeObject<User>(result);

                if (resultUser.UserId > 0)
                {

                    await PopAllTo(new HomeViewmodel());
                }
                else
                {
                    Console.WriteLine("Wrong username/password");
                }
            }
            else
            {
                Console.WriteLine("Please fill in all empty boxes");
            }
        }

        public async Task PopAllTo(HomeViewmodel viewmodel)
        {
            if (viewmodel == null) return;
            HomePage page = new HomePage(viewmodel); //replace 'page' with the page you want to reset to
            if (page == null) return;
            Navigation.InsertPageBefore(page, Navigation.NavigationStack.First());
            await Navigation.PopToRootAsync();
        }
    }
}