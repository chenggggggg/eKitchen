using eKitchen.Models;
using eKitchen.Services;
using eKitchen.Views;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace eKitchen.Viewmodels
{
    public class LoginViewmodel : INotifyPropertyChanged
    {

        public User UserAccount { get; set; }

        public INavigation Navigation { get; set; }

        public DataService DataService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewmodel(INavigation navigation)
        {
            Navigation = navigation;
            DataService = new DataService();
            UserAccount = new User();
        }

        /// <summary>
        /// Gets an existing user from the Secure Storage, returns null if none exist.
        /// </summary>
        /// <returns></returns>
        public async Task LoadRememberedUser()
        {
            try
            {
                UserAccount = new User();
                UserAccount.UserName = Preferences.Get(nameof(UserAccount.UserName), string.Empty);
                var password = await SecureStorage.GetAsync("password");
                UserAccount.Password = password;
            }
            catch(Exception ex)
            {
                throw ex;
            }             
        }


        /// <summary>
        /// Loads existing user data and logs in automaticly.
        /// </summary>
        public async Task AutoLogin()
        {
            await LoadRememberedUser();
            await LoginUser();
        }

        /// <summary>
        /// HTTP Post request for finding an user with the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task LoginUser()
        {
            if (UserAccount.UserName.Length > 0 && UserAccount.Password.Length > 0)
            {
                try
                {
                    ////Convert object to json and PostAsync to web API. 
                    //string data = JsonConvert.SerializeObject(UserAccount);
                    //var json = new StringContent(data, Encoding.UTF8, "application/json");
                    //var responseString = await HttpClientService.Instance.HttpClient.PostAsync("https://10.0.2.2:44342/api/user/login", json);

                    ////Deserialize json response to object.
                    //var result = await responseString.Content.ReadAsStringAsync();
                    //UserAccount.UserId = JsonConvert.DeserializeObject<int>(result);
                    UserAccount = await DataService.LoginUser(UserAccount);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (UserAccount.UserId > 0)
                    {
                        await SaveUser();
                        await PopAllTo(new HomeViewmodel());
                    }
                    else
                    {
                        Console.WriteLine("Wrong username/password");
                    }
                }
            }
            else
            {
                Console.WriteLine("Please fill in all empty boxes");
            }
        }

        /// <summary>
        /// Save user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public async Task SaveUser()
        {
            if (UserAccount.UserName.Length > 0 && UserAccount.Password.Length > 0)
            {
                try
                {
                    Preferences.Set(nameof(UserAccount.UserName), UserAccount.UserName);
                    await SecureStorage.SetAsync("password", UserAccount.Password);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Resets the navigation stack and pushes to Home page.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
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
