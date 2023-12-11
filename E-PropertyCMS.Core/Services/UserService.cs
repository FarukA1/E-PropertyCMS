using System;
using E_PropertyCMS.Core.Application.ConvertDtoToDomain;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.Caching;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Model;
using Microsoft.Extensions.Caching.Memory;

namespace E_PropertyCMS.Core.Services
{
	public class UserService : IUserService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDtoToDomain _dtoToDomain;
        private readonly IUserRepository _userRepository;

        private string userCacheKey = "clients";

        public UserService(IMemoryCache memoryCache, IDtoToDomain dtoToDomain, IUserRepository userRepository)
		{
            _memoryCache = memoryCache;
            _dtoToDomain = dtoToDomain;
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {

            var cacheService = new CacheService<User>(_memoryCache, userCacheKey);

            var users = await cacheService.GetCacheData();

            if (users == null)
            {
                users = await _userRepository.GetUsers();

                await cacheService.StoreCacheData(users);
            }

            if (users == null)
            {
                throw new EPropertyCMSException();
            }

            return users;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            return user;
        }

        public async Task<User> StoreUser(UserDto dto)
        {
            var user = await _dtoToDomain.GetUser(dto);

            await _userRepository.StoreUser(user);

            return user;
        }
    }
}

