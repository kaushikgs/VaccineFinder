using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineFinderLibrary.Models;

namespace VaccineFinderLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlotFetchController : ControllerBase
    {
        private readonly ILogger<SlotFetchController> _logger;

        public SlotFetchController(ILogger<SlotFetchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public CalendarResponse Get()
        {
            var rng = new Random();
            return new CalendarResponse
            {
                centers = Enumerable.Range(1, 5).Select(index => new Center
                {
                    center_id = rng.Next(),
                    state_name = "Random state " + index.ToString()
                })
            };
        }
    }
}
