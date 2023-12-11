using System;
namespace E_PropertyCMS.Core.Application.Dto
{
	public class AccountDto
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

