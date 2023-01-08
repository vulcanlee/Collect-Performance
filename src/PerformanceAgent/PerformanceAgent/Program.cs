using PerformanceAgent;
using PerformanceAgent.Models;
using PerformanceAgent.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.Configure<PerformanceConfiguration>(
            hostContext.Configuration.GetSection("Performance"));

        services.AddTransient<PerformanceService>();
    })
    .Build();

host.Run();
