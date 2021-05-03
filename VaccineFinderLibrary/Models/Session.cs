using System.Collections.Generic;

namespace VaccineFinderLibrary.Models
{
    public class Session
    {
        public string session_id { get; set; }
        public string date { get; set; }
        public int available_capacity { get; set; }
        public int min_age_limit { get; set; }
        public string vaccine { get; set; }
        public IEnumerable<string> slots { get; set; }
    }
}