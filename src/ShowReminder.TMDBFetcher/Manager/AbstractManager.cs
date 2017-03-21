using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShowReminder.TMDBFetcher.Model;

namespace ShowReminder.TMDBFetcher.Manager
{
    public abstract class AbstractManager
    {

        protected readonly TMDBSettings Settings;

        protected HttpClientHandler ClientHandler { get; set; }

        protected HttpClient Client { get; set; }

        protected string BaseUrl => Settings.BaseUrl;

        private JsonSerializerSettings _jsonSettings;

        protected AbstractManager(TMDBSettings settings)
        {
            Settings = settings;
            Initialize();
        }

        protected void Initialize()
        {
            ClientHandler = new HttpClientHandler();
            Client = new HttpClient(ClientHandler);

            _jsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        protected T ParseJsonFromResponse<T>(HttpResponseMessage response)
        {
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return (T) JsonConvert.DeserializeObject(jsonResponse, typeof(T), _jsonSettings);
        }

        protected T GetRequest<T>(string url)
        {
            url = AppendApiKeyToUrl(url);
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var response = Client.SendAsync(request).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return ParseJsonFromResponse<T>(response);
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return default(T);
                    }
                    throw new Exception("Failed to make request. " + response.ReasonPhrase);
                }
            }
        }

        protected static HttpContent CreateRequestBody(Object value)
        {
            string json = JsonConvert.SerializeObject(value);
            var content = new ByteArrayContent(Encoding.ASCII.GetBytes(json));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        protected T PostRequest<T>(string url, Object body)
        {
            url = AppendApiKeyToUrl(url);
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = CreateRequestBody(body);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = Client.SendAsync(request).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return ParseJsonFromResponse<T>(response);
                    }
                    throw new Exception("Failed to make request. " + response.ReasonPhrase);
                }
            }
        }

        protected string AppendApiKeyToUrl(string url)
        {
            string apiKeyParam = "api_key=" + Settings.ApiKey;

            if (url.Contains("?"))
            {
                url += "&" + apiKeyParam;
            }
            else
            {
                url += "?" + apiKeyParam;
            }
            return url;
        }

    }
}
