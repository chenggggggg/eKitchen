using eKitchen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eKitchen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private HttpClient client = new HttpClient();
        private User user = new User();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text.Length > 0 && passwordEntry.Text.Length > 0)
            {                
                GetUser(usernameEntry.Text, passwordEntry.Text);
                if (user.UserId > 0)
                {
                    Console.WriteLine(user.UserId);
                }
                else
                {
                    Console.Write("Wrong username/password");
                }
            }
            else
            {
                Console.WriteLine("Please fill in all empty boxes");
            }
        }

        protected async void GetUser(string username, string password)
        {
            //var content = await _client.PostAsync("Login", );
            //var user = JsonConvert.DeserializeObject<User>(content);

            //_user = (User)user;



            var values = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://172.18.25.209:44342/api/user/login", content);

            Console.WriteLine("Posting...");

            var responseString = await response.Content.ReadAsStringAsync();

        }
    }
}