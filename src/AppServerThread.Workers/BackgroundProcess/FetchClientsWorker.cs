using AppServerThread.Workers.BackgroundServices;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppServerThread.Workers.BackgroundProcess
{
    public class FetchClientsWorker : BackgroundService
    {
        private readonly FetchClientService _fetchClientService;

        public FetchClientsWorker(FetchClientService fetchClientService)
        {
            this._fetchClientService = fetchClientService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _fetchClientService.StartSync();
                    await Task.CompletedTask;
                    break;

                    //await Task.Delay(6000 * 1000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
