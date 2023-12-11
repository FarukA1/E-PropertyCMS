using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_PropertyCMS.Api.App_Start;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_PropertyCMS.Api.Controllers
{
    [Route($"api/Properties")]
    [Authorize]
    [ApiController]
    [ValidateRequest]
    public class PropertyController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly IPropertyService _propertyService;

        public PropertyController(IUriService uriService, IPropertyService propertyService)
        {
            _uriService = uriService;
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties([FromQuery] PaginationFilter filter, string? fields, PropertyType? type, PropertyStatus? status)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var properties = new List<Property>();

            int count = 0;

            properties = await _propertyService.GetProperties();

            if (!properties.Any())
            {
                return NoContent();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(Guid id, string? fields)
        {
            var property = await _propertyService.GetPropertyById(id);

            if (property == null)
            {
                return NotFound();
            }

            if (fields != null)
            {
                var propertyShaped = DataShaper<Property>.GetShapedObject(property, fields);

                return Ok(new Response<object>(propertyShaped));
            }

            return Ok(new Response<Property>(property));
        }

        [HttpGet("{id}/rooms")]
        public async Task<IActionResult> GetPropertyRooms(Guid id, [FromQuery] PaginationFilter filter, string? fields)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var rooms = new List<Room>();

            int count = 0;

            rooms = await _propertyService.GetPropertyRooms(id);

            if (rooms == null)
            {
                return NotFound();
            }

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

        [HttpGet("{id}/rooms/{roomId}")]
        public async Task<IActionResult> GetRropertyRoomById(Guid id, Guid roomId, string? fields)
        {
            var property = await _propertyService.GetPropertyById(id);

            if(property == null)
            {
                throw new EPropertyCMSException($"Property {id} does not exist");
            }
            var room = await _propertyService.GetRoomById(roomId);

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

