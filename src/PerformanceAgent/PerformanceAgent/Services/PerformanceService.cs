using PerformanceAgent.Helpers;
using PerformanceAgent.Models;
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
        private readonly OutputFileHelper outputFileHelper;
        private readonly ConsoleHelper consoleHelper;
        private readonly MagicObject magicObject;
        PerformanceConfiguration configuration;
        PerformanceModel performanceModel;
        public PerformanceService(OutputFileHelper outputFileHelper,
            ConsoleHelper consoleHelper, MagicObject magicObject)
        {
            this.outputFileHelper = outputFileHelper;
            this.consoleHelper = consoleHelper;
            this.magicObject = magicObject;
        }

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
                    CounterName);
                return result;
            }
            else
            {
                var result = new PerformanceCounter(CategoryName,
                    CounterName, instanceName);
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
        public async void CollectPerformance(PerformanceConfiguration configuration)
        {
            this.configuration = configuration;
            performanceModel = new PerformanceModel();

            if (configuration.Action == MagicObject.PerformanceActionListAllCounter)
            {
                ListAllCounters();
            }
            else
            {
                CollectionPerformance();
            }
        }

        void CollectionPerformance()
        {
            #region 建立效能計數器物件集合
            foreach (var counterConfiguration in configuration.Counters)
            {
                var counter = GetPerformanceCounter(counterConfiguration.Category,
                    counterConfiguration.Counter, counterConfiguration.Instance);
                performanceModel.Counters.Add(counter);
            }
            #endregion

            #region 列出效能數據
            performanceModel.FirstReadValue();
            Console.WriteLine($"稍後一下");
            Thread.Sleep(1500);
            Console.WriteLine($"顯示現在效能");

            for (int i = 0; i < 50; i++)
            {
                performanceModel.RefreshPerformance();
                foreach (var item in performanceModel.Items)
                {
                    Console.Write(item.GetPerformancePath()+"  ");
                    consoleHelper.Output($"{item.Value}", ConsoleColor.Black, ConsoleColor.Yellow);
                    Console.WriteLine();
                }
                Thread.Sleep(2000);
                Console.WriteLine();
            }
            #endregion
        }

        void ListAllCounters()
        {
            if (configuration.ListAllCounterToFile)
            {
                string filename = outputFileHelper
                    .PrepareOutputFile(MagicObject.AllCounterWriteFolderName,
                    $"Counters.txt");
                var stream = consoleHelper.SetConsoleOutputToFile(filename);

                ListAllCountersToConsole();

                consoleHelper.ResetConsoleOutput(stream);
            }

            ListAllCountersToConsole();
        }

        void ListAllCountersToConsole()
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
        }
        #endregion
    }
}
