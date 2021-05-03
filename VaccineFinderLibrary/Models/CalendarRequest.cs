using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineFinderLibrary.Models
{
    public class CalendarRequest
    {
        public int center_id { get; set; }
        public string name { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string block_name { get; set; }
        public int pincode { get; set; }
        public string fee_type { get; set; }
        public string date { get; set; }
        public int min_age_limit { get; set; }
        public string vaccine { get; set; }
    }
}
