using PerformanceAgent;
using PerformanceAgent.Helpers;
using PerformanceAgent.Models;
using PerformanceAgent.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.Configure<PerformanceConfiguration>(
            hostContext.Configuration.GetSection("Performance"));

        services.AddTransient<PerformanceService>();
        services.AddSingleton<OutputFileHelper>();
        services.AddSingleton<MagicObject>();
        services.AddTransient<ConsoleHelper>();
    })
    .Build();

host.Run();
