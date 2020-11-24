using eKitchen.Models;
using eKitchen.Services;
using eKitchen.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace eKitchen.Viewmodels
{
    public class WelcomeViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User UserAccount { get; set; }
        public DataService DataService { get; set; }
        public bool IsLoading { get; set; } = false;

        public INavigation Navigation { get; set; }

        public WelcomeViewmodel()
        {
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
                UserAccount.UserName = Preferences.Get(nameof(UserAccount.UserName), string.Empty);
                var password = await SecureStorage.GetAsync("password");
                UserAccount.Password = password;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                Console.WriteLine("Invalid user");
            }
        }

        /// <summary>
        /// Search for existing user data and logs in automaticly.
        /// </summary>
        public async void AutoLogin(INavigation navigation)
        {
            IsLoading = true;

            await LoadRememberedUser();

            if (UserAccount.Password != null && UserAccount.Password.Length > 0)
            {
                Navigation = navigation;
                await LoginUser();
            }

            IsLoading = false;
        }

        /// <summary>
        /// Resets the navigation stack and pushes to Home page.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        public async Task PopAllTo(HomeViewmodel viewmodel)
        {
            if (viewmodel == null) return;
            HomePage page = new HomePage(viewmodel); 
            if (page == null) return;
            Navigation.InsertPageBefore(page, Navigation.NavigationStack.First());
            await Navigation.PopToRootAsync();
        }
    }
}
