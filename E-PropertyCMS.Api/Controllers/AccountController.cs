using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_PropertyCMS.Api.Controllers
{
    [Route($"api/Account")]
    [ApiController]
    [ValidateRequest]
    public class AccountController : ControllerBase
    {
        private readonly IManagementApiClient _managementApiClient;
        private readonly IUserService _userService;

        public AccountController(IManagementApiClient managementApiClient, IUserService userService)
        {
            _managementApiClient = managementApiClient;
            _userService = userService;
        }

        // [HttpGet("login")]
        // public async Task Login(string returnUrl = "/")
        // {
        //     var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        //         // Indicate here where Auth0 should redirect the user after a login.
        //         // Note that the resulting absolute Uri must be added to the
        //         // **Allowed Callback URLs** settings for the app.
        //         .WithRedirectUri(returnUrl)
        //         .Build();

        //     await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        // }

        ///[Authorize]
        // [HttpGet("logout")]
        // public async Task Logout()
        // {
        //     var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        //         // Indicate here where Auth0 should redirect the user after a logout.
        //         // Note that the resulting absolute Uri must be added to the
        //         // **Allowed Logout URLs** settings for the app.
        //         .WithRedirectUri("https://localhost:44483")
        //         .Build();

        //     await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        //     await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        // }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _managementApiClient.Users.GetAsync(userId);

            return Ok(user);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] AccountDto signupRequest)
        {
            try
            {
                // Create the user using Auth0 Management API
                var userCreateRequest = new UserCreateRequest
                {
                    Connection = "Username-Password-Authentication", // Auth0 connection name
                    Email = signupRequest.Email,
                    Password = signupRequest.Password,
                    FirstName = signupRequest.FirstName,
                    LastName = signupRequest.LastName,
                    UserName = signupRequest.UserName,
                    FullName = signupRequest.FirstName + " " + signupRequest.LastName,
                    Blocked = false
                };

                if(signupRequest.UserName == null)
                {
                    signupRequest.FirstName = userCreateRequest.UserName;
                }

                var createdUser = await _managementApiClient.Users.CreateAsync(userCreateRequest);

                // Optionally, you can perform additional actions or return success response
                //return Ok(new { Message = "User created successfully", UserId = createdUser.UserId });

                var newUser = new UserDto()
                {
                    UniqueId = createdUser.UserId,
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    UserName = createdUser.UserName,
                    Email = createdUser.Email,
                    Phone = signupRequest.Phone,
                };

                Uri picture = new Uri(createdUser.Picture);

                newUser.Picture = picture;

                var dbnewUser = await _userService.StoreUser(newUser);

                return Ok(dbnewUser);
            }
            catch (Exception ex)
            {
                // Handle errors and return appropriate response
                return BadRequest(new { Message = "Error creating user", Error = ex.Message });
            }
        }
    }

}

