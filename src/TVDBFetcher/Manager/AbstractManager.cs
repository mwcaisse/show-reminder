using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.TVDBFetcher.Manager
{
    public abstract class AbstractManager
    {

        protected const string BaseUrl = "https://api.thetvdb.com/";

        protected HttpClientHandler ClientHandler { get; set; }

        protected HttpClient Client { get; set; }

        private readonly AuthenticationParam _authenticationParam;

        protected AbstractManager(AuthenticationParam authParam)
        {
            _authenticationParam = authParam;
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
            AuthenticationStore.AuthenticationToken = token;
            SetAuthenticationTokenHeader(token);
        }

        protected void SetAuthenticationTokenHeader(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected T ParseJsonFromResponse<T>(HttpResponseMessage response)
        {
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            return (T)JsonConvert.DeserializeObject(jsonResponse, typeof(T));
        }

        protected T GetRequest<T>(string url, bool attemptLoginOnFail = true)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var response = Client.SendAsync(request).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return ParseJsonFromResponse<T>(response);
                    }
                    if (attemptLoginOnFail && response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        LoginOrSetToken();
                        return GetRequest<T>(url, false);
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

        protected T PostRequest<T>(string url, Object body, bool attemptLoginOnFail = true)
        {
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
                    if (attemptLoginOnFail && response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Login();
                        return PostRequest<T>(url, body, false);
                    }
                    throw new Exception("Failed to make request. " + response.ReasonPhrase);
                }
            }
        }

        protected void LoginOrSetToken()
        {
            if (AuthenticationStore.HasAuthenticationToken)
            {
                SetAuthenticationTokenHeader(AuthenticationStore.AuthenticationToken);
            }
            else
            {
                Login();
            }
        }

        protected string Login()
        {
            var response = PostRequest<AuthenticationResponse>(BaseUrl + "login", _authenticationParam, false);
            SetAuthentication(response);
            return response.Token;
        }

        protected AuthenticationResponse RefreshToken()
        {
            return null;
        }
    }
}
