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
using E_PropertyCMS.Core.Shaper;
using E_PropertyCMS.Core.CustomException;
using System.Dynamic;
using E_PropertyCMS.Core.Application.Dto;
using Microsoft.AspNetCore.Routing;
using E_PropertyCMS.Domain.Enumeration;

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
        public async Task<IActionResult> GetClients([FromQuery] PaginationFilter filter, string? fields, ClientType? type)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var clients = new List<Client>();

            int count = 0;

            if (type == null)
            {
                clients = await _clientService.GetClients(validFilter);
                count = await _clientService.ClientsTotal();
            }

            if (type != null)
            {
                clients = await _clientService.GetClientsByType(type, validFilter);
                count = await _clientService.ClientsTypeTotal(type);
            }

            if (!clients.Any())
            {
                throw new EPropertyCMSException("No Clients");
            }

            if (fields != null)
            {
                var clientsShaped = new List<object>();

                foreach (var client in clients)
                {
                    var clientShaped = DataShaper<Client>.GetShapedObject(client, fields);
                    clientsShaped.Add(clientShaped);
                }

                var responseShaped = PaginationHelper.CreatePagedResponse(clientsShaped, validFilter, count, _uriService, route);

                return Ok(responseShaped);
            }


            var response = PaginationHelper.CreatePagedResponse(clients, validFilter, count, _uriService, route); 

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid id, string? fields)
        {
            var client = await _clientService.GetClientById(id);

            if(client == null)
            {
                throw new EPropertyCMSException("Client does not exist");
            }

            if (fields != null)
            {
                var clientShaped = DataShaper<Client>.GetShapedObject(client, fields);

                return Ok(new Response<object>(clientShaped));
            }

            return Ok(new Response<Client>(client));
        }

        [HttpGet("{id}/properties")]
        public async Task<IActionResult> GetClientProperties(Guid id, string? fields)
        {
            var properties = await _clientService.GetClientProperties(id);

            if (!properties.Any())
            {
                throw new EPropertyCMSException($"{id} does not have any property");
            }

            if (fields != null)
            {
                var clientsShaped = new List<object>();

                foreach (var property in properties)
                {
                    var clientShaped = DataShaper<Property>.GetShapedObject(property, fields);
                    clientsShaped.Add(clientShaped);
                }

                return Ok(clientsShaped);
            }

            return Ok(new Response<List<Property>>(properties));
        }

        [HttpPost("create")]
        public async Task<IActionResult> StoreClient(ClientDto dto, string? fields)
        {
            var client = await _clientService.StoreClient(dto);

            if (client == null)
            {
                throw new EPropertyCMSException("Client does not exist");
            }

            if (fields != null)
            {
                var clientShaped = DataShaper<Client>.GetShapedObject(client, fields);

                return Ok(new Response<object>(clientShaped));
            }

            return Ok(new Response<Client>(client));
        }

        [HttpPost("import")]
        public async Task<IActionResult> StoreClient(List<ClientDto> dtos, string? fields)
        {
            if(fields == null)
            {
                fields = "id,firstName,lastName";
            }

            var responses = new List<object>();

            try
            {
                if(dtos.Any())
                {
                    foreach(var dto in dtos)
                    {
                        var client = await _clientService.StoreClient(dto);

                        var clientShaped = DataShaper<Client>.GetShapedObject(client, fields);

                        responses.Add(clientShaped);
                    }
                }
            }
            catch(Exception e)
            {
                throw new EPropertyCMSException();
            }

            return Ok(new Response<object>(responses));

        }
    }
}

