using System;
using System.Diagnostics;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class ClientDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDbModel Address { get; set; }
        public ClientType ClientType { get; set; }

        public List<PropertyDbModel> Properties { get; set; }

        public Client AddToDomain()
        {
            var client = new Client()
            {
                Id = Key,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address.AddToDomain(),
                ClientType = ClientType
            };

            foreach(var property in Properties)
            {
                client.Properties.Add(property.AddToDomain());
            }

            return client;
        }

        public void AddFromDomain(Client client)
        {
            FirstName = client.FirstName;
            LastName = client.LastName;
            Email = client.Email;
            Phone = client.Phone;
            ClientType = client.ClientType;
        }
    }
}

