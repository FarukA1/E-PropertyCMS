using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Helper;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Core.Shaper;
using E_PropertyCMS.Core.Wrappers;
using E_PropertyCMS.Domain.Filter;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_PropertyCMS.Api.Controllers {

    [Route($"api/Roles")]
    // [Authorize]
    [ApiController]
    [ValidateRequest]
    public class RoleController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly IUriService _uriService;
        private readonly IManagementApiClient _managementApiClient;

        public RoleController(IConfiguration configuration, IUriService uriService, IManagementApiClient managementApiClient)
        {
            _configuration = configuration;
            _uriService = uriService;
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Roles([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var getRolesRequest = new GetRolesRequest();

            var roles = (List<Role>)await _managementApiClient.Roles.GetAllAsync(getRolesRequest);

            if (!roles.Any())
            {
                return NoContent();
            }
 
            roles = roles.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                        .Take(filter.PageSize).ToList();
            
            var response = PaginationHelper.CreatePagedResponse(roles, validFilter, roles.Count(), _uriService, route); 

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id, string? fields)
        {
            var role = await _managementApiClient.Roles.GetAsync(id);

            if(role == null)
            {
                return NotFound();
            }

            if (fields != null)
            {
                var roleShaped = DataShaper<Role>.GetShapedObject(role, fields);

                return Ok(new Response<object>(roleShaped));
            }

            return Ok(new Response<Role>(role));
        }


        [HttpPost("create")]
        public async Task<IActionResult> StoreRole(RoleCreateRequest dto) 
        {
            if(dto.Name == null) 
            {
                throw new EPropertyCMSException("New role name cannot be empty");
            }

            var newRole = new Role();

            try
            {
                var create = await _managementApiClient.Roles.CreateAsync(dto);

                if(create != null) 
                {
                    newRole = create;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return Ok(newRole);
        }

        [HttpGet("{id}/permissions")]
        public async Task<IActionResult> GetRolePermissionsById(string id, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var paging = new PaginationInfo();

            var permissions = (List<Permission>)await _managementApiClient.Roles.GetPermissionsAsync(id,paging);

            if (!permissions.Any())
            {
                return NoContent();
            }

            permissions = permissions.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(filter.PageSize).ToList();
            
            var response = PaginationHelper.CreatePagedResponse(permissions, validFilter, permissions.Count(), _uriService, route); 

            return Ok(response);
        }

        // [HttpPost("{id}/permissions/create")]
        // public async Task<IActionResult> StorePermissionForRole(string id, string Name, string? Description) 
        // {
        //     var dto = new Permission();

        //     if (Name == null) {
        //         throw new EPropertyCMSException("New permission name cannot be empty");
        //     }

        //     if(Description != null) 
        //     {
        //         dto.Description = Description;
        //     }

        //     dto.Name = Name;

        //     dto.Identifier = _configuration["Api-Auth0:Audience"];

        //     var newPermission = _managementApiClient.Roles.AssignPermissionsAsync(id,dto);

        //     return Ok();
        // }

    }
}