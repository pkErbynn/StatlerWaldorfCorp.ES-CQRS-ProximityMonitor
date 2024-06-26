using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.TeamService
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private readonly TeamServiceOptionSettings teamServiceOptions;
        private readonly ILogger logger;
        private HttpClient httpClient;

        public HttpTeamServiceClient(
            ILogger<HttpTeamServiceClient> logger,
            IOptions<TeamServiceOptionSettings> serviceOptions)
        {
            this.logger = logger;               
            this.teamServiceOptions = serviceOptions.Value;
            
            logger.LogInformation("Team Service HTTP client using URL {0}", teamServiceOptions.Url);

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(teamServiceOptions.Url);
        }

        public Team GetTeam(Guid teamId)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/teams/{0}", teamId)).Result;

            Team teamResponse = null;
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                teamResponse = JsonConvert.DeserializeObject<Team>(json);                
            }
            return teamResponse;
        }

        public Member GetMember(Guid teamId, Guid memberId)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/teams/{0}/members/{1}", teamId, memberId)).Result;

            Member memberResponse = null;
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                memberResponse = JsonConvert.DeserializeObject<Member>(json);
            }
            
            return memberResponse;
        }
    }
}