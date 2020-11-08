using eKitchen.Models;
using eKitchen.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace eKitchen.Viewmodels
{
    public class HomeViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DataService DataService { get; set; }

        public List<IProduct> Products { get; set; }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Products = DataService.GetSearchResults(query);
        });
    }
}
