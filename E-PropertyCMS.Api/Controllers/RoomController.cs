using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_PropertyCMS.Api.App_Start;
using E_PropertyCMS.Core.Helper;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Core.Shaper;
using E_PropertyCMS.Core.Wrappers;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_PropertyCMS.Api.Controllers
{
    [Route($"api/Rooms")]
    [ApiController]
    [ValidateRequest]
    public class RoomController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly IPropertyService _propertyService;

        public RoomController(IUriService uriService, IPropertyService propertyService)
        {
            _uriService = uriService;
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] PaginationFilter filter, string? fields)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var rooms = new List<Room>();

            int count = 0;

            rooms = await _propertyService.GetRooms();

            if (!rooms.Any())
            {
                return NoContent();
            }

            count = rooms.Count();

            rooms = rooms.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(filter.PageSize).ToList();

            if (fields != null)
            {
                var roomsShaped = new List<object>();

                foreach (var room in rooms)
                {
                    var roomShaped = DataShaper<Room>.GetShapedObject(room, fields);
                    roomsShaped.Add(roomShaped);
                }

                var responseShaped = PaginationHelper.CreatePagedResponse(roomsShaped, validFilter, count, _uriService, route);

                return Ok(responseShaped);
            }


            var response = PaginationHelper.CreatePagedResponse(rooms, validFilter, count, _uriService, route);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(Guid id, string? fields)
        {
            var room = await _propertyService.GetRoomById(id);

            if (room == null)
            {
                return NotFound();
            }

            if (fields != null)
            {
                var roomShaped = DataShaper<Room>.GetShapedObject(room, fields);

                return Ok(new Response<object>(roomShaped));
            }

            return Ok(new Response<Room>(room));
        }
    }
}

