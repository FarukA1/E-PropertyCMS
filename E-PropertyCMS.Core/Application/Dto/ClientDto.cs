using System;
using E_PropertyCMS.Domain.Enumeration;

namespace E_PropertyCMS.Core.Application.Dto
{
	public class ClientDto
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
        public ClientType ClientType { get; set; }

        public List<PropertyDto> Properties { get; set; }
    }
}

