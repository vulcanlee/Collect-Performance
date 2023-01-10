using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceAgent.Models
{
    public class PerformanceConfiguration
    {
        public string Action { get; set; } = "";
        public bool ListAllCounterToFile { get; set; }
        public List<PerformanceCounterConfiguration> Counters { get; set; } = new();
    }

    public class PerformanceCounterConfiguration
    {
        public string Category { get; set; } = "";
        public string Counter { get; set; } = "";
        public string Instance { get; set; } = "";
        public string MachineName { get; set; } = "";
    }
}
