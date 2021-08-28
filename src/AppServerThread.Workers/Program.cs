using AppServerThread.Workers.BackgroundProcess;
using AppServerThread.Workers.BackgroundServices;
using AppServerThread.Workers.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<ClientMongoRepository>();
        services.AddScoped<FetchClientService>();
        services.AddTransient<ApiClientService>();
        services.AddHostedService<FetchClientsWorker>();

        services.AddRefitClient<IClientApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://webApi/api"));
    })
    .Build();

await host.RunAsync();