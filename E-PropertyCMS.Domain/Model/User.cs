using System;
namespace E_PropertyCMS.Domain.Model
{
	public class User
	{
        public Guid Id { get; set; }
        public string UniqueId { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Uri? Picture { get; set; }
        public bool IsBlocked { get; set; }
    }
}

