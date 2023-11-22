using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Core.Helper;

namespace E_PropertyCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateRequest]
    public class ClientController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly IClientService _clientService;

        public ClientController(IUriService uriService, IClientService clientService)
        {
            _uriService = uriService;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var clients = await _clientService.GetClients(validFilter);

            var total = clients.Count();

            var clientsResponse = PaginationHelper.CreatePagedResponse(clients, validFilter, total, _uriService, route); 

            return Ok(clientsResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid Id)
        {
            var client = await _clientService.GetClientById(Id);

            return Ok(new Response<Client>(client));
        }
    }
}

