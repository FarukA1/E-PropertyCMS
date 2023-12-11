using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Repositories
{
	public interface IUserRepository
	{
        Task<List<User>> GetUsers();
        Task<User> GetUserById(Guid Id);
        Task<User> StoreUser(User user);
    }
}

