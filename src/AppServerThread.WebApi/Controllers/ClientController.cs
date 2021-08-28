using AppServerThread.Core;
using AppServerThread.Core.Results;
using AppServerThread.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppServerThread.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            this._clientService = clientService;
        }

        [HttpGet]
        public DataResult<IList<Client>> Get([FromQuery] Options options)
        {
            return this._clientService.Get(options);
        }
    }
}
