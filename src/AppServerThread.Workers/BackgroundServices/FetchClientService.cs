using AppServerThread.Workers.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AppServerThread.Workers.BackgroundServices
{
    public class FetchClientService
    {
        public readonly ILogger<string> logger;
        private readonly ApiClientService _apiClientService;
        private readonly ClientMongoRepository _clientMongoRepository;
        private static readonly object lockObj = new();

        public FetchClientService(ApiClientService apiClientService, ClientMongoRepository clientMongoRepository, ILogger<string> logger)
        {
            this._apiClientService = apiClientService;
            this._clientMongoRepository = clientMongoRepository;
            this.logger = logger;
        }

        private static void CriarLog()
        {
            using var sw = new StreamWriter(Environment.CurrentDirectory + "/Clients.txt", true);
            sw.WriteLine(DateTime.Now.ToString()); 
        }

        public void StartSync()
        {
            CriarLog();

            var totalRequest = this._apiClientService.GetTotalPage();
            if (totalRequest > 0)
            {
                var semaphore = new SemaphoreSlim(5);
                var tasks = new Task[totalRequest];

                for (int i = 0; i < totalRequest; i++)
                {
                    var index = i + 1;
                    tasks[i] = Task.Run(() =>
                    {
                        semaphore.Wait();
                        try
                        {
                            var results = this._apiClientService.GetList(index);
                            foreach (var client in results)
                            {
                                lock (lockObj)
                                {
                                    _clientMongoRepository.Save(client);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex.Message);
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    });
                }
                Task.WaitAll(tasks);
                CriarLog();
            }
        }
    }
}
