using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Repositories
{
	public interface IPropertyRepository
	{
        Task<List<Property>> GetProperties();
        Task<Property> GetPropertyById(Guid Id);
        Task<List<Room>> GetPropertyRooms(Guid propertyId);
        Task<List<Room>> GetRooms();
        Task<Room> GetRoomById(Guid Id);
    }
}

