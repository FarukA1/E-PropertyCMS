using System;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Repositories
{
	public class PropertyRepository : IPropertyRepository
    {
        private readonly ICoreContext _coreContext;

        public PropertyRepository(ICoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<List<Property>> GetProperties()
        {
            var properties = new List<Property>();

            var propertiesDbModel = await _coreContext.Property
                .Include(v => v.Client)
                .Include(v => v.Address)
                .Include(v => v.Rooms)
                .ToListAsync();

            foreach (var propertyDbModel in propertiesDbModel)
            {
                properties.Add(propertyDbModel?.AddToDomain());
            }

            return properties;
        }

        public async Task<Property> GetPropertyById(Guid Id)
        {
            var property = await _coreContext.Property
                .Where(v => v.Key == Id)
                .Include(v => v.Address)
                 .Include(v => v.Rooms)
                .FirstOrDefaultAsync();

            return property?.AddToDomain();
        }

        public async Task<List<Room>> GetPropertyRooms(Guid propertyId)
        {
            var roomList = new List<Room>();

            var rooms = await _coreContext.Room
                .Include(v => v.Property)
                .Where(v => v.Property.Key == propertyId)
                .ToListAsync();

            if (!rooms.Any())
            {
                return null;
            }

            foreach (var roomdb in rooms)
            {
                var roomdomain = roomdb.AddToDomain();

                roomList.Add(roomdomain);
            }

            return roomList;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = new List<Room>();

            var roomsDbModel = await _coreContext.Room
                .Include(v => v.Property)
                .ToListAsync();

            foreach (var roomDbModel in roomsDbModel)
            {
                rooms.Add(roomDbModel?.AddToDomain());
            }

            return rooms;
        }

        public async Task<Room> GetRoomById(Guid Id)
        {
            var room = await _coreContext.Room
                .Where(v => v.Key == Id)
                .Include(v => v.Property)
                .FirstOrDefaultAsync();

            return room?.AddToDomain();
        }
    }
}

