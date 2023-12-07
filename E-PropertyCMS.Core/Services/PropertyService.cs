using System;
using E_PropertyCMS.Core.Application.ConvertDtoToDomain;
using E_PropertyCMS.Core.Caching;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Model;
using Microsoft.Extensions.Caching.Memory;

namespace E_PropertyCMS.Core.Services
{
	public class PropertyService : IPropertyService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDtoToDomain _dtoToDomain;
        private readonly IPropertyRepository _propertyRepository;

        private string propertyCacheKey = "properties";
        private string roomCacheKey = "rooms";

        public PropertyService(IMemoryCache memoryCache, IDtoToDomain dtoToDomain, IPropertyRepository propertyRepository)
		{
            _memoryCache = memoryCache;
            _dtoToDomain = dtoToDomain;
            _propertyRepository = propertyRepository;
        }

        public async Task<List<Property>> GetProperties()
        {
            var cacheService = new CacheService<Property>(_memoryCache, propertyCacheKey);

            var properties = await cacheService.GetCacheData();

            if (properties == null)
            {
                properties = await _propertyRepository.GetProperties();
            }

            if (properties == null)
            {
                throw new EPropertyCMSException();
            }

            return properties;
        }

        public async Task<Property> GetPropertyById(Guid propertyId)
        {
            var property = await _propertyRepository.GetPropertyById(propertyId);

            return property;
        }

        public async Task<List<Room>> GetPropertyRooms(Guid propertyId)
        {
            var property = await GetPropertyById(propertyId);

            if (property == null)
            {
                throw new EPropertyCMSException($"Property {propertyId} does exist");
            }

            var rooms = await _propertyRepository.GetPropertyRooms(propertyId);

            if (!rooms.Any())
            {
                return null;
            }

            return rooms;
        }

        public async Task<List<Room>> GetRooms()
        {
            var cacheService = new CacheService<Room>(_memoryCache, roomCacheKey);

            var rooms = await cacheService.GetCacheData();

            if (rooms == null)
            {
                rooms = await _propertyRepository.GetRooms();
            }

            if (rooms == null)
            {
                throw new EPropertyCMSException();
            }

            return rooms;
        }

        public async Task<Room> GetRoomById(Guid roomId)
        {
            var room = await _propertyRepository.GetRoomById(roomId);

            return room;
        }
    }
}

