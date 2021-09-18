using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Web.ExternalAPI.RestClient
{
    public class RestClient : IRestClient
    {
        public RestClient()
        {
        }

        public async Task<string> GetAsync(string requestUri, Dictionary<string, string> additionalHeaders = null)
        {
            string result = string.Empty;
            using (HttpClientHandler httpClientHandler = new())
            {
                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    result = await httpClient.GetStringAsync(requestUri);
                }
            }
            return result;
        }

        public async Task<string> PostAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            string result = string.Empty;
            using (HttpClientHandler httpClientHandler = new())
            {
                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    result = await httpClient.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(request))
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }).Result.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        public async Task<string> DeleteAsync(string requestUri, Dictionary<string, string> additionalHeaders = null)
        {
            string result = string.Empty;
            using (HttpClientHandler httpClientHandler = new())
            {
                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    var httpResponseMessage = await httpClient.DeleteAsync(requestUri);
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        public async Task<string> PutAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            string result = string.Empty;
            using (HttpClientHandler httpClientHandler = new ())
            {
                using (HttpClient httpClient = new (httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);

                    var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    var httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponseMessage = await httpClient.PutAsync(requestUri, httpContent);
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        public async Task<string> PatchAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            string result = string.Empty;
            using (HttpClientHandler httpClientHandler = new())
            {
                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    var httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponseMessage = await httpClient.PatchAsync(requestUri, httpContent);
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        private static void AddHeaders(HttpClient httpClient, Dictionary<string, string> additionalHeaders)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //No additional headers to be added
            if (additionalHeaders == null)
                return;

            foreach (KeyValuePair<string, string> current in additionalHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(current.Key, current.Value);
            }
        }
    }
}
