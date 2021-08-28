using AppServerThread.Core;
using AppServerThread.Core.Results;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServerThread.Workers.BackgroundServices
{
    public class ApiClientService
    {
        private readonly IClientApi _clientApi;
        private const int QTD_RECORDS = 10;

        public ApiClientService(IClientApi clientApi)
        {
            this._clientApi = clientApi;
        }

        public int GetTotalPage()
        {
            var data = this._clientApi.GetClients(GetOptions()).Result?.MetaData;
            return data?.TotalPages ?? 0;
        }

        public IList<Client> GetList(int page)
        {
            return this._clientApi.GetClients(GetOptions(page)).Result?.List;
        }

        private static Options GetOptions(int page = 1)
        {
            return new Options { Page = page, Total = QTD_RECORDS };
        }
    }

    public interface IClientApi
    {
        [Get("/client?page={options.Page}&total={options.Total}")]
        Task<DataResult<IList<Client>>> GetClients(Options options);
    }
}
