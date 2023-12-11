using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class UserDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string UniqueId { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Uri? Picture { get; set; }
        public bool IsBlocked { get; set; }

        public User AddToDomain()
        {
            var user = new User()
            {
                Id = Key,
                UniqueId = UniqueId,
                Title = Title,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Phone = Phone,
                Picture = Picture,
                IsBlocked = IsBlocked
            };

            return user;
        }

        public void AddFromDomain(User user)
        {
            UniqueId = user.UniqueId;
            Title = user.Title;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Email = user.Email;
            Phone = user.Phone;
            Picture = user.Picture;
            IsBlocked = user.IsBlocked;
        }
    }
}

