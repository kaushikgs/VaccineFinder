using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineFinderLibrary.Models
{
    public class CalendarResponse
    {
        public IEnumerable<Center> centers { get; set; }
    }
}
