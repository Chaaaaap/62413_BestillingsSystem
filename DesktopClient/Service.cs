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
            var passwordHash = StringUtility.HashString(StringUtility.SecureStringToStringConverter(securePassword));

            

            var values = new Dictionary<string, string>
            {
                { "secureUsername", username },
                { "securePassword", passwordHash }
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
                { "isadmin", user.IsAdmin.ToString() }
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
        #endregion
    }
}
