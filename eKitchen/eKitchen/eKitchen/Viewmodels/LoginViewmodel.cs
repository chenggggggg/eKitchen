﻿using eKitchen.Models;
using eKitchen.Services;
using eKitchen.Views;
using System;
using System.ComponentModel;
using System.Linq;
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

        public LoginViewmodel(INavigation navigation)
        {
            Navigation = navigation;
            DataService = new DataService();
            UserAccount = new User();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
