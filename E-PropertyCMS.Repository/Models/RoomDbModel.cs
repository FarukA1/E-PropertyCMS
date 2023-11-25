using System;
using E_PropertyCMS.Domain.Model;
using System.Diagnostics;

namespace E_PropertyCMS.Repository.Models
{
	public class RoomDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public PropertyDbModel Property { get; set; }
        public int PropertyId { get; set; }
        public string RoomType { get; set; }
        public string? Description { get; set; }

        public Room AddToDomain()
        {
            var room = new Room()
            {
                Id = Key,
                RoomType = RoomType,
                Description = Description
            };

            return room;
        }

        public void AddFromDomain(Room room)
        {
            RoomType = room.RoomType;
            Description = room.Description;
        }
    }
}

