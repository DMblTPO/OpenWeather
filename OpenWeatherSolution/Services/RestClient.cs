using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenWeatherSolution.Services
{
    public class RestUrl
    {
        public string Base { get; set; }
        public string Route { get; set; }
    }
    public class RestClient
    {
        private static HttpClient GetClient(string baseUrl)
        {
            var client = new HttpClient {BaseAddress = new Uri(baseUrl)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static async Task<T> ProceedResponse<T>(HttpResponseMessage response) where T : class
        {
            T result = null;
            await response.Content.ReadAsStringAsync().ContinueWith(x =>
            {
                if (x.IsFaulted)
                    throw x.Exception ?? new Exception("Something goes wrong during reading of the REST response");

                result = JsonConvert.DeserializeObject<T>(x.Result);
            });
            return result;
        }

        public static async Task<T> GetAsync<T>(RestUrl api) where T : class
        {
            using (var client = GetClient(api.Base))
            {
                var response = await client.GetAsync(api.Route).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await ProceedResponse<T>(response);
            }
        }

        public static async Task<T> GetAllAsync<T>(RestUrl api) where T : class
        {
            using (var client = GetClient(api.Base))
            {
                var response = await client.GetAsync(api.Route).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await ProceedResponse<T>(response);
            }
        }

        public static async Task<T> PostAsync<T>(RestUrl api, T postObject) where T : class
        {
            using (var client = GetClient(api.Base))
            {
                var response = await client.PostAsync(api.Route, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await ProceedResponse<T>(response);
            }
        }

        public static async Task PutAsync<T>(RestUrl api, T putObject) where T : class
        {
            using (var client = GetClient(api.Base))
            {
                var response = await client.PutAsync(api.Route, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteAsync(RestUrl api)
        {
            using (var client = GetClient(api.Base))
            {
                var response = await client.DeleteAsync(api.Route).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}