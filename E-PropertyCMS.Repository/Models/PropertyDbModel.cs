using System;
using System.Diagnostics.Metrics;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class PropertyDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public ClientDbModel Client { get; set; }
        public int clientId { get; set; }
        public AddressDbModel Address { get; set; }
        public int AddressId { get; set; }
        public string Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public string? Description { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public List<RoomDbModel> Rooms { get; set; }

        public Property AddToDomain()
        {
            var property = new Property()
            {
                Id = Key,
                Price = Price,
                PropertyType = PropertyType,
                Description = Description,
                PropertyStatus = PropertyStatus
            };

            if(Address != null)
            {
                property.Address = Address.AddToDomain();
            }

            if(Rooms != null)
            {
                foreach(var room in Rooms)
                {
                    property.Rooms.Add(room.AddToDomain());
                }
            }

            return property;
        }

        public void AddFromDomain(Property property)
        {
            Price = property.Price;
            PropertyType = property.PropertyType;
            Description = property.Description;
            PropertyStatus = property.PropertyStatus;
        }
    }
}

