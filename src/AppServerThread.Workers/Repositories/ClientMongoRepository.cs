using AppServerThread.Core;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace AppServerThread.Workers.Repositories
{
    public class ClientMongoRepository
    {
        public readonly ILogger<string> logger;

        public ClientMongoRepository(ILogger<string> logger)
        {
            this.logger = logger;
        }

        public void Save(Client client)
        {
            using var sw = new StreamWriter(Environment.CurrentDirectory  + "/Clients.txt", true);
            sw.WriteLine(client);
        }
    }
}
