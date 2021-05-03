using System.Collections.Generic;

namespace VaccineFinderLibrary.Models
{
    public class Session
    {
        public string session_id { get; set; }
        public string date { get; set; }
        public int available_capacity { get; set; } = 0;
        public int min_age_limit { get; set; } = 0;
        public string vaccine { get; set; }
        public IEnumerable<string> slots { get; set; }
    }
}