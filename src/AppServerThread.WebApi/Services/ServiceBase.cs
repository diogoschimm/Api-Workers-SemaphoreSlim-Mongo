using AppServerThread.Core;
using AppServerThread.Core.Results;
using AppServerThread.WebApi.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AppServerThread.WebApi.Services
{
    public abstract  class ServiceBase
    {
        protected DataResult<IList<T>> Get<T>(IList<T> results, Options options)
        {
            var resultsPagging = results.Skip(options.GetInitialPage()).Take(options.Total).ToList();

            var dataResult = new DataResult<IList<T>>
            {
                List = resultsPagging,
                MetaData = new MetaData
                {
                    Count = resultsPagging.Count,
                    Page = options.Page,
                    Total = results.Count,
                    TotalPages = PaggingUtils.GetTotalPages(results, options),
                }
            };

            return dataResult;
        }
    }
}
