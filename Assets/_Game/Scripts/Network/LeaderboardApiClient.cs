using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Trivia
{
    public class LeaderboardApiClient
    {
        HttpClient client;
        HttpRequestMessage request;

        public LeaderboardApiClient()
        {
            client = new HttpClient();
            request = new HttpRequestMessage();
        }

        public async Task<LeaderboardPageData> GetLeaderboardPageDataAsync(int page)
        {
            return await GenerateGetRequestAsync(page);
        }

        private async Task<LeaderboardPageData> GenerateGetRequestAsync(int page)
        {
            // Query Paramaters
            var query = new Dictionary<string, string>()
            {
                { "page", page.ToString() }
            };

            using (var response = await client.GetAsync(Utils.GetUriWithQueryString(Constants.API_URL, query)))
            {
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                LeaderboardPageData leaderboardPageData = JsonConvert.DeserializeObject<LeaderboardPageData>(content);

                return leaderboardPageData;
            }
        }
    }
}