using System;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Services
{
	public interface IUserService
	{
        Task<List<User>> GetUsers();
        Task<User> GetUserById(Guid userId);
        Task<User> StoreUser(UserDto dto);
    }
}

