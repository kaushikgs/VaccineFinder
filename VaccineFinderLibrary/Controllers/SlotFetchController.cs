using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VaccineFinderLibrary.Models;

namespace VaccineFinderLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlotFetchController : ControllerBase
    {
        private readonly ILogger<SlotFetchController> _logger;
        private CowinAPIHandler apiHandler;

        public SlotFetchController(ILogger<SlotFetchController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            apiHandler = new CowinAPIHandler(clientFactory);
        }

        [HttpPost]
        public async Task<CalendarResponse> PostAsync(CalendarRequest request)
        {
            CalendarResponse apiResponse = await apiHandler.CalendarDistrictSearchAsync(request.state_name, request.district_name, request.date);
            CalendarResponse filtered = FilterResults(apiResponse, request);
            CalendarResponse sorted = SortResults(filtered);
            return sorted;
        }

        private CalendarResponse FilterResults(CalendarResponse response, CalendarRequest request)
        {
            IEnumerable<Center> filteredCenters = response.centers;

            foreach(var center in filteredCenters)
            {
                center.sessions = center.sessions.Where(session => session.available_capacity > 0);

                if (request.min_age_limit != 0)
                {
                    center.sessions = center.sessions.Where(session => session.min_age_limit <= request.min_age_limit);
                }
                if (!string.IsNullOrEmpty(request.vaccine))
                {
                    center.sessions = center.sessions.Where(session => session.vaccine == request.vaccine);
                }

            }
            filteredCenters = filteredCenters.Where(center => center.sessions.Any());

            if (!string.IsNullOrEmpty(request.fee_type))
            {
                filteredCenters = filteredCenters.Where(center => center.fee_type == request.fee_type);
            }

            return new CalendarResponse { centers = filteredCenters };
        }

        private CalendarResponse SortResults(CalendarResponse response)
        {
            response.centers = response.centers.ToList().OrderBy(center => center.sessions.Select(session => session.date).Min());
            return response;
        }
    }
}
