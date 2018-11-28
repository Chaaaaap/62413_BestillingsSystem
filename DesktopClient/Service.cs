using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using Common;
using Common.Utils;

namespace DesktopClient
{
    public static class Service
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        #region Users
        public static async Task<User> Login(string username, SecureString securePassword)
        {
            //var passwordHash = StringUtility.HashString(StringUtility.SecureStringToStringConverter(securePassword));

            var password = StringUtility.SecureStringToStringConverter(securePassword);

            var values = new Dictionary<string, string>
            {
                { "Username", username },
                { "Password", password }
            };

            HttpContent content = new FormUrlEncodedContent(values);

            var response = await HttpClient.PostAsync(ApplicationInfo.WebApiKey + "/login", content);

            if (response.IsSuccessStatusCode)
            {
                return await JsonUtility.ParseJson<User>(response);
            }
            return null;
        }

        public static async void RegisterUser(User user)
        {
            var values = new Dictionary<string, string>
            {
                { "username", user.Username },
                { "password", user.Password },
                { "email", user.Email }
            };
            HttpContent content = new FormUrlEncodedContent(values);
            var response = await HttpClient.PostAsync(ApplicationInfo.WebApiKey + "/user", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException();
            }
        }

        public static async Task<User> AdminCreateUser(User user)
        {
            var values = new Dictionary<string, string>
            {
                { "username", user.Username },
                { "password", user.Password },
                { "email", user.Email },
                { "isadmin", user.IsAdmin.ToString()}
            };
            HttpContent content = new FormUrlEncodedContent(values);
            var response = await HttpClient.PostAsync(ApplicationInfo.WebApiKey + "/user", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException();
            }
            return user;
        }

        public static async Task<List<User>> GetAllUsers()
        {
            var response = await HttpClient.GetAsync(ApplicationInfo.WebApiKey + "/user");

            if (response.IsSuccessStatusCode)
            {
                return await JsonUtility.ParseJson<List<User>>(response);
            }
            return null;
        }

        public static async Task<User> UpdateUser(User user)
        {
            var values = new Dictionary<string, string>
            {
                { "username", user.Username },
                { "password", user.Password },
                { "email", user.Email },
                { "isadmin", user.IsAdmin.ToString()}
            };
            HttpContent content = new FormUrlEncodedContent(values);

            var response = await HttpClient.PutAsync(ApplicationInfo.WebApiKey + "/user/" + user.Id, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException();
            }
            return user;
        }
        #endregion

        #region Items

        public static async Task<List<Item>> GetAllItems()
        {
            var response = await HttpClient.GetAsync(ApplicationInfo.WebApiKey + "/item");

            if (response.IsSuccessStatusCode)
            {
                return await JsonUtility.ParseJson<List<Item>>(response);
            }
            return null;
        }

        public static async Task<Item> UpdateItem(Item item)
        {
            var values = new Dictionary<string, string>
            {
                { "name", item.Name },
                { "price", item.Price.ToString()},
                { "amount", item.Amount.ToString()}
            };
            HttpContent content = new FormUrlEncodedContent(values);

            var response = await HttpClient.PutAsync(ApplicationInfo.WebApiKey + "/item/" + item.Id, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException();
            }
            return item;
        }

        public static async Task<Item> CreateItem(Item item)
        {
            var values = new Dictionary<string, string>
            {
                { "name", item.Name },
                { "Amount", item.Amount.ToString()},
                { "Price", item.Price.ToString()},
            };
            HttpContent content = new FormUrlEncodedContent(values);
            var response = await HttpClient.PostAsync(ApplicationInfo.WebApiKey + "/item", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException();
            }

            return item;
        }
        #endregion
    }
}
