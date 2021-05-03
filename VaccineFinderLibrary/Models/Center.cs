using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineFinderLibrary.Models
{
    public class Center
    {
        public int center_id { get; set; }
        public string name { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string block_name { get; set; }
        public int pincode { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string fee_type { get; set; }
        public IEnumerable<VaccineFee> vaccine_fees { get; set; }
        public IEnumerable<Session> sessions { get; set; }
    }
}
