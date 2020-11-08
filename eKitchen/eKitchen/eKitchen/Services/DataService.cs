using eKitchen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eKitchen.Services
{
    public class DataService
    {
        public async Task<User> LoginUser(User user)
        {
            //Convert object to json and PostAsync to web API. 
            string data = JsonConvert.SerializeObject(user);
            var json = new StringContent(data, Encoding.UTF8, "application/json");
            var responseString = await HttpClientService.Instance.HttpClient.PostAsync("https://10.0.2.2:44342/api/user/login", json);

            //Deserialize json response to object.
            var result = await responseString.Content.ReadAsStringAsync();
            user.UserId = JsonConvert.DeserializeObject<int>(result);
            return user;
        }

        public List<IProduct> GetSearchResults(string query)
        {
            return new List<IProduct>();
        }
    }
}
