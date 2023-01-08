using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceAgent.Models
{
    public class PerformanceModel
    {
        public List<PerformanceCounter> Counters { get; set; } = new();
        public List<PerformanceItemModel> Items { get; set; } = new();
        public void FirstReadValue()
        {
            Items.Clear();
            foreach (var item in Counters)
            {
                var value = item.NextValue();
                var perfItem = new PerformanceItemModel()
                {
                    Category = item.CategoryName,
                    Counter = item.CounterName,
                    Instance = item.InstanceName,
                    Value = 0,
                };
                Items.Add(perfItem);
            }
        }

        public void RefreshPerformance()
        {
            for (int i = 0; i < Counters.Count; i++)
            {
                Items[i].Value = Counters[i].NextValue();
            }
        }

        public string GetPerformancePath(PerformanceCounter performanceCounter)
        {
            string result = "";
            if (string.IsNullOrEmpty(performanceCounter.InstanceName))
            {
                result = $"{performanceCounter.CategoryName}" +
                    $"\\{performanceCounter.CounterName}";
            }
            else
            {
                result = $"{performanceCounter.CategoryName}" +
                    $"({performanceCounter.InstanceName})" +
                    $"\\{performanceCounter.CounterName}";
            }
            return result;
        }
    }

}
