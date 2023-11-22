using System;
using E_PropertyCMS.Domain.Enumeration;

namespace E_PropertyCMS.Core.Application.Dto
{
	public class PropertyDto
	{
        public AddressDto Address { get; set; }
        public string Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Description { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }
}

