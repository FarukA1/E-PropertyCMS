using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Helper;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Core.Shaper;
using E_PropertyCMS.Core.Wrappers;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace E_PropertyCMS.Api.Controllers
{
    [Route($"api/Cases")]
    [Authorize]
    [ApiController]
    [ValidateRequest]
    public class CaseController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly ICaseService _caseService;

        public CaseController(ICaseService caseService, IUriService uriService)
        {
           _caseService = caseService;
           _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCases([FromQuery] PaginationFilter filter, string? fields, CaseStatus? status, string? searchQuery)
        {
            // [FromQuery] PaginationFilter filter, string? fields, CaseType? type, string? searchQuery
            // CaseStatus? status, 
            var route = Url.Action(null, null, new
            {
                fields,
                status,
                searchQuery
            });

            var validFilter = new PaginationFilter
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var cases = new List<Case>();

            int count = 0;

            if(searchQuery == null) 
            {
                cases = await _caseService.GetCases();
            }
            else 
            {
                cases = await _caseService.Search(searchQuery);
            }

            if (!cases.Any())
            {
                return Ok();
                // return Ok(new Response<Client>(clients));
            }

            count = cases.Count();

            // if (type != null)
            // {
            //     cases = cases.Where(v => v.CaseType == type).ToList();
            //     count = cases.Count();
            // }

            if (status != null)
            {
                cases = cases.Where(v => v.CaseStatus == status).ToList();
                count = cases.Count();
            }

            cases = cases.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                 .Take(filter.PageSize).ToList();

            if (fields != null)
            {
                var clientsShaped = new List<object>();

                foreach (var kase in cases)
                {
                    var clientShaped = DataShaper<Case>.GetShapedObject(kase, fields);
                    clientsShaped.Add(clientShaped);
                }

                var responseShaped = PaginationHelper.CreatePagedResponse(clientsShaped, validFilter, count, _uriService, route);

                return Ok(responseShaped);
            }


            var response = PaginationHelper.CreatePagedResponse(cases, validFilter, count, _uriService, route); 

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaseById(Guid id, string? fields)
        {
            var kase = await _caseService.GetCaseById(id);

            if(kase == null)
            {
                return NotFound();
            }

            if (fields != null)
            {
                var clientShaped = DataShaper<Case>.GetShapedObject(kase, fields);

                return Ok(new Response<object>(clientShaped));
            }

            return Ok(new Response<Case>(kase));
        }

        [HttpPost("create")]
        public async Task<IActionResult> StoreCase(CaseDto dto, string? fields)
        {
            var kase = await _caseService.StoreCase(dto);

            if (kase == null)
            {
                throw new EPropertyCMSException("Case was not created");
            }

            if (fields != null)
            {
                var caseShaped = DataShaper<Case>.GetShapedObject(kase, fields);

                return Ok(new Response<object>(caseShaped));
            }

            return Ok(new Response<Case>(kase));
        }

        [HttpPost("create/casetype")]
        public async Task<IActionResult> StoreCaseType(CaseTypeDto dto, string? fields)
        {
            var caseType = await _caseService.StoreCaseType(dto);

            if (caseType == null)
            {
                throw new EPropertyCMSException("Case Type was not created");
            }

            if (fields != null)
            {
                var caseTypeShaped = DataShaper<CaseType>.GetShapedObject(caseType, fields);

                return Ok(new Response<object>(caseTypeShaped));
            }

            return Ok(new Response<CaseType>(caseType));
        }
    }
}

