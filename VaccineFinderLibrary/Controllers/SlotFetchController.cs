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

        [HttpPost]
        public CalendarResponse Post(Center filter)
        {
            var rng = new Random();
            return new CalendarResponse
            {
                centers = Enumerable.Range(1, 5).Select(index => filter)
            };
        }
    }
}
