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
    [Route($"api/Dashboard")]
    [Authorize]
    [ApiController]
    [ValidateRequest]
    public class DashboardController : ControllerBase 
    {
        private readonly IClientService _clientService;
        private readonly ICaseService _caseService;
        private readonly IPropertyService _propertyService;

        public DashboardController(IClientService clientService, ICaseService caseService, IPropertyService propertyService)
        {
            _clientService = clientService;
            _caseService = caseService;
            _propertyService = propertyService;
        }

        [HttpGet("detail")]
        public async Task<IActionResult> Detail() 
        {
            var clients = await _clientService.GetClients();
            var cases = await _caseService.GetCases();
            var properties = await _propertyService.GetProperties();

            var latestClients = clients.OrderByDescending(x => x.CreatedOn).Take(5).ToList();
            var latestCases = cases.OrderByDescending(x => x.LastModifiedOn).Take(5).ToList();

            var dashboard = new Dashboard() 
            {
                NumberofClients = clients.Count(),
                NumberofCases = cases.Count(),
                NumberofProperties = properties.Count()
            };

            if(latestClients.Any()) 
            {
                dashboard.Clients = latestClients;
            }

            if(latestCases.Any()) 
            {
                dashboard.Cases = latestCases;
            }

            return Ok(new Response<Dashboard>(dashboard));
        }
    }
}