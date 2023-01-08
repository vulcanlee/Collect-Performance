using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceAgent.Services
{
    public class PerformanceService
    {
        #region 效能監視器基本 API
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
            string CounterName, string instanceName, string machineName)
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
        #endregion

        #region 效能監視器要執行的動作
        public void ListAllCounters()
        {
            var allCatetories = GetAllCategory("");
            foreach (var category in allCatetories)
            {
                Console.WriteLine($"  分類：{category.CategoryName}");
                Console.WriteLine($"  目的：{category.CategoryHelp}");
                Console.WriteLine();
                var allInstances = GetAllInstance(category);
                var allCounters = GetAllCounter(category, allInstances.Count == 0 ? "" : allInstances[0]);

                foreach (var counter in allCounters)
                {
                    Console.WriteLine($"計數器:{counter.CounterName}");
                    Console.WriteLine($"      {counter.CounterHelp}");
                }

                if (allInstances.Count > 0)
                {
                    Console.WriteLine($"     ----------------------------------------");
                    foreach (var instance in allInstances)
                    {
                        Console.WriteLine($"     實例：{instance}");
                    }
                }
                Console.WriteLine();
            }
            #endregion
        }
    }
}
