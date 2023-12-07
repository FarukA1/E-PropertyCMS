using System;
using E_PropertyCMS.Domain.Enumeration;

namespace E_PropertyCMS.Domain.Model
{
	public class Property
	{
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Address Address { get; set; }
        public string Price { get; set; }
        public PropertyType PropertyType { get; set; } 
        public string? Description { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public List<Room> Rooms { get; set; }

        public Property()
        {
            Rooms = new List<Room>();
        }
    }
}

