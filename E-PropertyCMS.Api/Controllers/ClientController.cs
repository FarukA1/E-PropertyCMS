using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Core.Helper;

namespace E_PropertyCMS.Api.Controllers
{
    [ValidateRequest]
    [RoutePrefix("api/Client")]
    public class ClientController : ApiController
    {
        private readonly IUriService _uriService;
        private readonly IClientService _clientService;

        public ClientController(IUriService uriService, IClientService clientService)
        {
            _uriService = uriService;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetClients([FromQuery] PaginationFilter filter)
        {
            var route = Request.RequestUri.ToString();

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var clients = await _clientService.GetClients(validFilter);

            var total = clients.Count();

            var clientsResponse = PaginationHelper.CreatePagedResponse(clients, validFilter, total, _uriService, route); 

            return Ok(clientsResponse);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IHttpActionResult> GetClientById(Guid Id)
        {
            var client = await _clientService.GetClientById(Id);

            return Ok(new Response<Client>(client));
        }
    }
}

