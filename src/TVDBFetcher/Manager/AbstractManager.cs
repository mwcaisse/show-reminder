using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.TVDBFetcher.Manager
{
    public class AbstractManager
    {

        protected HttpClientHandler ClientHandler { get; set; }

        protected HttpClient Client { get; set; }

        private string AuthenticationToken { get; set; }

        private DateTime AuthenticationTokenRetrieved { get; set; }

        public AbstractManager()
        {
            Initialize();
        }

        protected void Initialize()
        {
            ClientHandler = new HttpClientHandler();
            Client = new HttpClient(ClientHandler);
        }

        protected void SetAuthentication(AuthenticationResponse authenticationResponse)
        {
            SetAuthentication(authenticationResponse.Token);
        }

        protected void SetAuthentication(string token)
        {
            AuthenticationToken = token;
            AuthenticationTokenRetrieved = DateTime.Now;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected T ParseJsonFromResponse<T>(HttpResponseMessage response)
        {
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return (T)JsonConvert.DeserializeObject(jsonResponse, typeof(T));
        }

        protected T GetRequest<T>(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var response = Client.SendAsync(request).Result)
                {
                    return ParseJsonFromResponse<T>(response);
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
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = CreateRequestBody(body);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = Client.SendAsync(request).Result)
                {
                    return ParseJsonFromResponse<T>(response);
                }
            }
        }

        public string Login(AuthenticationParam param)
        {
            var response = PostRequest<AuthenticationResponse>(" https://api.thetvdb.com/login", param);
            SetAuthentication(response);
            return response.Token;
        }

        protected AuthenticationResponse RefreshToken()
        {
            return null;
        }
    }
}
