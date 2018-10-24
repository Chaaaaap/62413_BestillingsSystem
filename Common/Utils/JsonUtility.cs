using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Utils
{
    public static class JsonUtility
    {
        public static async Task<T> ParseJson<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                /* Parses the response to string */
                var dataObjects = await response.Content.ReadAsStringAsync();

                /* Returns the new Task which */
                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(dataObjects));
            }
            throw new Exception("An error occured while parsing Json.");
        }
    }
}
