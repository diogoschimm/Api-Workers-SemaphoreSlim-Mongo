using AppServerThread.Core;
using AppServerThread.Core.Results;
using AppServerThread.WebApi.Data;
using System.Collections.Generic;

namespace AppServerThread.WebApi.Services
{
    public class ClientService : ServiceBase 
    {
        private readonly ClientRepository _clientRepository;

        public ClientService(ClientRepository clientRepository)
        {
            this._clientRepository = clientRepository;
        }

        public DataResult<IList<Client>> Get(Options options)
        {
            var results = this._clientRepository.GetResults();
            return Get(results, options);
        }  
    }
}
