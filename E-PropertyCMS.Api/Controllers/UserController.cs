using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Helper;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Core.Shaper;
using E_PropertyCMS.Core.Wrappers;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_PropertyCMS.Api.Controllers
{
    [Route($"api/User")]
    [Authorize]
    [ApiController]
    [ValidateRequest]
    public class UserController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly IUserService _userService;
        private readonly IManagementApiClient _managementApiClient;

        public UserController(IUriService uriService, IUserService userService, IManagementApiClient managementApiClient)
        {
            _uriService = uriService;
            _userService = userService;
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationFilter filter, string? fields)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var users = new List<User>();

            int count = 0;

            users = await _userService.GetUsers();

            if (!users.Any())
            {
                return NoContent();
            }

            count = users.Count();

            users = users.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(filter.PageSize).ToList();

            if (fields != null)
            {
                var usersShaped = new List<object>();

                foreach (var user in users)
                {
                    var userShaped = DataShaper<User>.GetShapedObject(user, fields);
                    usersShaped.Add(userShaped);
                }

                var responseShaped = PaginationHelper.CreatePagedResponse(usersShaped, validFilter, count, _uriService, route);

                return Ok(responseShaped);
            }


            var response = PaginationHelper.CreatePagedResponse(users, validFilter, count, _uriService, route);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id, string? fields)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (fields != null)
            {
                var userShaped = DataShaper<User>.GetShapedObject(user, fields);

                return Ok(new Response<object>(userShaped));
            }

            return Ok(new Response<User>(user));
        }

        [HttpGet("{id}/Auth0/{uniqueId}")]
        public async Task<IActionResult> GetUserByIdFromAuth0(Guid id, string uniqueId, string? fields)
        {
            var localUser = await _userService.GetUserById(id);

            if (localUser == null)
            {
                return NotFound();
            }

            if(uniqueId != localUser.UniqueId)
            {
                throw new EPropertyCMSException($"The unique Id ({uniqueId}) for {localUser.Id} is incorrect");
            }

            var auth0UserDetails = new Auth0.ManagementApi.Models.User();

            try
            {
                var auth0User = await _managementApiClient.Users.GetAsync(localUser.UniqueId);

                if (auth0User == null)
                {
                    return NotFound();
                }

                if (fields != null)
                {
                    var userShaped = DataShaper<Auth0.ManagementApi.Models.User>.GetShapedObject(auth0User, fields);

                    return Ok(new Response<object>(userShaped));
                }

                auth0UserDetails = auth0User;
            }
            catch (Exception ex)
            {
                // Handle errors and return appropriate response
                return BadRequest(new { Message = "Error getting user details", Error = ex.Message });
            }

            return Ok(new Response<Auth0.ManagementApi.Models.User>(auth0UserDetails));
        }
    }
}

