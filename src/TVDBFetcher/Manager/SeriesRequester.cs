using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TVDBFetcher.Model;

namespace TVDBFetcher.Manager
{
    public class SeriesRequester
    {


        public BasicEpisode GetNextAiringEpisode(int showId)
        {

            return null;
        }

        public SeriesData GetSeries(int id)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "xxx Token");
                    var url = "https://api.thetvdb.com/series/" + id;
                    using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                    {
                        using (var response = httpClient.SendAsync(request).Result)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            SeriesData responseData = JsonConvert.DeserializeObject<SeriesData>(jsonResponse);
                            return responseData;
                        }
                    }

                }
            }
        }
    }
}
