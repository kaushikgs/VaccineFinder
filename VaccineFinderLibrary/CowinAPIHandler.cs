using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VaccineFinderLibrary.Models;

namespace VaccineFinderLibrary
{
    public class CowinAPIHandler
    {
        private Dictionary<string, int> States { get; set; }
        private Dictionary<int, Dictionary<string, int>> Districts { get; set; }
        
        private const string ApiHost = "https://cdn-api.co-vin.in/";
        private const string StatesRoute = "api/v2/admin/location/states";
        private const string DistrictsRoute = "api/v2/admin/location/districts/";
        private const string CalendarByDistrictRoute = "api/v2/appointment/sessions/public/calendarByDistrict";
        private readonly Dictionary<string, string> LanguageParam = new Dictionary<string, string> { { "Accept-Language","en_US" } };

        private readonly IHttpClientFactory _clientFactory;

        public CowinAPIHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            Initialize();
        }

        public async Task<CalendarResponse> CalendarDistrictSearchAsync(string stateName, string districtName, string date)
        {
            int stateId = States[stateName];
            if (!Districts.ContainsKey(stateId))
            {
                GetDistricts(stateId);
            }
            int districtId = Districts[stateId][districtName];

            var calendarByDistrictUrl = ApiHost + CalendarByDistrictRoute;
            var parameters = new Dictionary<string, string>(LanguageParam);
            parameters.Add("district_id", districtId.ToString());
            parameters.Add("date", date);
            CalendarResponse calendarResponse = await HttpGetAsync<CalendarResponse>(calendarByDistrictUrl, parameters);
            return calendarResponse;
        }

        private void Initialize()
        {
            var statesUrl = ApiHost + StatesRoute;
            StatesResponse statesResponse = HttpGetAsync<StatesResponse>(statesUrl, LanguageParam).Result;
            States = new Dictionary<string, int>();
            if (statesResponse != null)
            {
                foreach (var state in statesResponse.states)
                {
                    States.Add(state.state_name, state.state_id);
                }
            }
            Districts = new Dictionary<int, Dictionary<string, int>>();
        }

        private async void GetDistricts(int stateId)
        {
            var districtsUrl = ApiHost + DistrictsRoute + stateId.ToString();
            DistrictsResponse districtsResponse = HttpGetAsync<DistrictsResponse>(districtsUrl, LanguageParam).Result;
            var districtsDict = new Dictionary<string, int>();
            if(districtsResponse != null)
            {
                foreach (var district in districtsResponse.districts)
                {
                    districtsDict.Add(district.district_name, district.district_id);
                }
            }
            Districts[stateId] = districtsDict;
        }

        private async Task<TResponse> HttpGetAsync<TResponse>(string url, Dictionary<string,string> parameters)
        {
            var paramsString = "?" + string.Join('&', parameters.Select(pair => $"{pair.Key}={pair.Value}"));
            var request = new HttpRequestMessage(HttpMethod.Get, url + paramsString);
            request.Headers.Add("Accept", "application/json");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var responseObj = await JsonSerializer.DeserializeAsync
                    <TResponse>(responseStream);
                return responseObj;
            }
            return default(TResponse);
        }

    }
}
