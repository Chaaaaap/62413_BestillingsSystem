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
        //private static Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //private static readonly string ApiKey = ConfigurationManager.AppSettings["WebApiKey"];

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
                //return await JsonUtility.ParseJson<User>(response);
            }
        }

    }
}
