using PerformanceAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceAgent.Helpers
{
    public static class ConfigurationHelper
    {
        public static PerformanceConfiguration Prepare(this PerformanceConfiguration configuration)
        {
            var allCounters = configuration.Counters.ToList();
            foreach (var item in allCounters)
            {
                if(string.IsNullOrEmpty(item.Category))
                    configuration.Counters.Remove(item);
            }
            return configuration;
        }
    }
}
