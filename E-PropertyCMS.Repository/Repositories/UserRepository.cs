using System;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Repositories
{
	public class UserRepository : IUserRepository
    {
        private readonly ICoreContext _coreContext;

        public UserRepository(ICoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = new List<User>();

            var usersDbModel = await _coreContext.User
                .ToListAsync();

            foreach (var userDbModel in usersDbModel)
            {
                users.Add(userDbModel.AddToDomain());
            }

            return users;
        }

        public async Task<User> GetUserById(Guid Id)
        {
            var user = await _coreContext.User
                .FirstOrDefaultAsync(v => v.Key == Id);

            return user?.AddToDomain();
        }

        public async Task<User> GetUserByAuthId(string UniqueId)
        {
            var user = await _coreContext.User
                .FirstOrDefaultAsync(v => v.UniqueId == UniqueId);

            return user?.AddToDomain();
        }

        public async Task<User> StoreUser(User user)
        {
            var newUser = new UserDbModel()
            {
                Key = user.Id
            };

            newUser.AddFromDomain(user);

            _coreContext.User.Add(newUser);

            await _coreContext.SaveChangesAsync();

            return newUser?.AddToDomain();
        }
    }
}

