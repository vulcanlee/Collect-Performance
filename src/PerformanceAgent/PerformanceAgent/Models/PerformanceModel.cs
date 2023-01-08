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
        public List<PerformanceCounterCategory> GetAllCategory(string machineName)
        {
            if (string.IsNullOrEmpty(machineName))
                machineName = Environment.MachineName;

            List<PerformanceCounterCategory> categories =
                PerformanceCounterCategory.GetCategories(machineName)
                .OrderBy(x => x.CategoryName).ToList();

            return categories;
        }

        public List<string> GetAllInstance(PerformanceCounterCategory category)
        {
            var instanceNames = category.GetInstanceNames().ToList();
            return instanceNames;
        }

        public List<PerformanceCounter> GetAllCounter(PerformanceCounterCategory category,
            string instanceName)
        {
            if (string.IsNullOrEmpty(instanceName))
            {
                List<PerformanceCounter> counter = category.GetCounters().ToList();
                return counter;
            }
            else
            {
                List<PerformanceCounter> counter = category.GetCounters(instanceName).ToList();
                return counter;
            }

        }

        public PerformanceCounter GetPerformanceCounter(PerformanceCounter counter,
            string instanceName)
        {

            if (string.IsNullOrEmpty(instanceName))
            {
                var result = new PerformanceCounter(counter.CategoryName,
                    counter.CounterName, instanceName);
                return result;
            }
            else
            {
                var result = new PerformanceCounter(counter.CategoryName,
                    counter.CounterName);
                return result;
            }
        }

        public PerformanceCounter GetPerformanceCounter(string CategoryName,
            string CounterName, string instanceName)
        {

            if (string.IsNullOrEmpty(instanceName))
            {
                var result = new PerformanceCounter(CategoryName,
                    CounterName, instanceName);
                return result;
            }
            else
            {
                var result = new PerformanceCounter(CategoryName,
                    CounterName);
                return result;
            }
        }

        public PerformanceCounter GetPerformanceCounter(string CategoryName,
            string CounterName, string instanceName,string machineName)
        {

            if (string.IsNullOrEmpty(instanceName))
            {
                var result = new PerformanceCounter(CategoryName,
                    CounterName, instanceName, machineName);
                return result;
            }
            else
            {
                var result = new PerformanceCounter(CategoryName,
                    CounterName, String.Empty, machineName);
                return result;
            }
        }
    }
}
