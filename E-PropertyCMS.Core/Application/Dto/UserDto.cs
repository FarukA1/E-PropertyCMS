using System;
namespace E_PropertyCMS.Core.Application.Dto
{
	public class UserDto
	{
        public string UniqueId { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Uri? Picture { get; set; }
    }
}

