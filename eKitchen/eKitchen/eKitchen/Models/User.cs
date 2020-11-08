using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eKitchen.Models
{
    public class User : INotifyPropertyChanged
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public User()
        {

        }
    }
}
