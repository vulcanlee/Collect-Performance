using Microsoft.Extensions.Options;
using PerformanceAgent.Helpers;
using PerformanceAgent.Models;
using PerformanceAgent.Services;

namespace PerformanceAgent
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PerformanceService performanceService;
        private readonly IOptions<PerformanceConfiguration> options;

        public Worker(ILogger<Worker> logger,
            PerformanceService performanceService,
            IOptions<PerformanceConfiguration> options)
        {
            _logger = logger;
            this.performanceService = performanceService;
            this.options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1500);

            options.Value.Prepare();

            performanceService.CollectPerformance(options.Value);

            Environment.Exit(0);
        }
    }
}