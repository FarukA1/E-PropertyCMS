using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Services
{
	public interface IPropertyService
	{
        Task<List<Property>> GetProperties();
        Task<Property> GetPropertyById(Guid propertyId);
        Task<List<Room>> GetPropertyRooms(Guid propertyId);
        Task<List<Room>> GetRooms();
        Task<Room> GetRoomById(Guid roomId);
    }
}

