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
using Microsoft.AspNetCore.Authorization;

namespace E_PropertyCMS.Api.Controllers
{
    [Route($"api/Clients")]
    [Authorize]
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
        public async Task<IActionResult> GetClients([FromQuery] PaginationFilter filter, string? fields, ClientType? type, string? searchQuery)
        {
            var route = Url.Action(null, null, new
            {
                fields,
                type,
                searchQuery
            });

            var validFilter = new PaginationFilter
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var clients = new List<Client>();

            int count = 0;

            if(searchQuery == null) 
            {
                clients = await _clientService.GetClients();
            }
            else 
            {
                clients = await _clientService.Search(searchQuery);
            }

            if (!clients.Any())
            {
                return Ok();
                // return Ok(new Response<Client>(clients));
            }

            count = clients.Count();

            if (type != null)
            {
                clients = clients.Where(v => v.ClientType == type).ToList();
                count = clients.Count();
            }

            clients = clients.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                 .Take(filter.PageSize).ToList();

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

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromQuery] PaginationFilter filter, string? fields, ClientType? type, SearchDto search)
        {
            var route = Url.Action(null, null, new
            {
                fields,
                type
            });

            var validFilter = new PaginationFilter
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var clients = new List<Client>();

            int count = 0;

            clients = await _clientService.Search(search.SearchQuery);

            if (!clients.Any())
            {
                return NoContent();
            }

            count = clients.Count();

            if (type != null)
            {
                clients = clients.Where(v => v.ClientType == type).ToList();
                count = clients.Count();
            }

            clients = clients.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                 .Take(filter.PageSize).ToList();

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
                return NotFound();
            }

            if (fields != null)
            {
                var clientShaped = DataShaper<Client>.GetShapedObject(client, fields);

                return Ok(new Response<object>(clientShaped));
            }

            return Ok(new Response<Client>(client));
        }


        [HttpGet("{id}/properties")]
        public async Task<IActionResult> GetClientProperties(Guid id, [FromQuery] PaginationFilter filter, string? fields, PropertyType? type, PropertyStatus? status)
        {
            var route = Url.Action(null, null, new
            {
                fields,
                type,
                status
            });

            var validFilter = new PaginationFilter
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var properties = new List<Property>();

            int count = 0;

            properties = await _clientService.GetClientProperties(id);

            if (!properties.Any())
            {
                return NotFound();
            }

            count = properties.Count();

            if (type != null)
            {
                properties = properties.Where(v => v.PropertyType == type).ToList();
                count = properties.Count();
            }

            if (status != null)
            {
                properties = properties.Where(v => v.PropertyStatus == status).ToList();
                count = properties.Count();
            }

            properties = properties.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(filter.PageSize).ToList();

            if (fields != null)
            {
                var propertysShaped = new List<object>();

                foreach (var property in properties)
                {
                    var propertyShaped = DataShaper<Property>.GetShapedObject(property, fields);
                    propertysShaped.Add(propertyShaped);
                }

                var responseShaped = PaginationHelper.CreatePagedResponse(propertysShaped, validFilter, count, _uriService, route);

                return Ok(responseShaped);
            }

            var response = PaginationHelper.CreatePagedResponse(properties, validFilter, count, _uriService, route);

            return Ok(response);
        }

        // [HttpGet("{id}/cases")]
        // public async Task<IActionResult> GetClientCases(Guid id, [FromQuery] PaginationFilter filter, string? fields, CaseType? type, CaseStatus? status)
        // {
        //     var route = Url.Action(null, null, new
        //     {
        //         fields,
        //         type,
        //         status
        //     });

        //     var validFilter = new PaginationFilter
        //     {
        //         PageNumber = filter.PageNumber,
        //         PageSize = filter.PageSize
        //     };

        //     var cases = new List<Case>();

        //     int count = 0;

        //     cases = await _clientService.GetClientCases(id);

        //     if (!cases.Any())
        //     {
        //         return NotFound();
        //     }

        //     count = cases.Count();

        //     if (type != null)
        //     {
        //         cases = cases.Where(v => v.CaseType == type).ToList();
        //         count = cases.Count();
        //     }

        //     if (status != null)
        //     {
        //         cases = cases.Where(v => v.CaseStatus == status).ToList();
        //         count = cases.Count();
        //     }

        //     cases = cases.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        //         .Take(filter.PageSize).ToList();

        //     if (fields != null)
        //     {
        //         var casesShaped = new List<object>();

        //         foreach (var kase in cases)
        //         {
        //             var caseShaped = DataShaper<Case>.GetShapedObject(kase, fields);
        //             casesShaped.Add(caseShaped);
        //         }

        //         var responseShaped = PaginationHelper.CreatePagedResponse(casesShaped, validFilter, count, _uriService, route);

        //         return Ok(responseShaped);
        //     }

        //     var response = PaginationHelper.CreatePagedResponse(cases, validFilter, count, _uriService, route);

        //     return Ok(response);
        // }


        [HttpPost("create")]
        public async Task<IActionResult> StoreClient(ClientDto dto, string? fields)
        {
            var client = await _clientService.StoreClient(dto);

            if (client == null)
            {
                throw new EPropertyCMSException("Client was not created");
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

