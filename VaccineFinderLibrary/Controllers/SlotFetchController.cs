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
            return await apiHandler.CalendarDistrictSearchAsync(request.state_name, request.district_name, request.date);
        }
    }
}
