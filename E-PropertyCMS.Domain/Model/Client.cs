using System;
using E_PropertyCMS.Domain.Enumeration;

namespace E_PropertyCMS.Domain.Model
{
	public class Client
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public ClientType ClientType { get; set; }

        public List<Property> Properties { get; set; }
    }
}

