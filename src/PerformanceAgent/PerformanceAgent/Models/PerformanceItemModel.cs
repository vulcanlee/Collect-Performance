using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceAgent.Models
{
    public class PerformanceItemModel
    {
        public string MachineName { get; set; } = "";
        public string Category { get; set; } = "";
        public string Counter { get; set; } = "";
        public string Instance { get; set; } = "";
        public float Value { get; set; } = 0;

        public string GetPerformancePath()
        {
            string result = "";
            if (string.IsNullOrEmpty(Instance))
            {
                result = $"{Category}" +
                    $"\\{Counter}";
            }
            else
            {
                result = $"{Category}" +
                    $"({Instance})" +
                    $"\\{Counter}";
            }
            return result;
        }
    }
}
