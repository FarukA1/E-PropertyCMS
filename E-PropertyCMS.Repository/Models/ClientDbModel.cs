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
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDbModel Address { get; set; }
        public int AddressId { get; set; }
        public ClientType ClientType { get; set; }

        public List<PropertyDbModel> Properties { get; set; }

        public Client AddToDomain()
        {
            var client = new Client()
            {
                Id = Key,
                Title = Title,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                ClientType = ClientType
            };

            if(Address != null)
            {
                client.Address = Address.AddToDomain();
            }

            if(Properties != null)
            {
                foreach(var property in Properties)
                {
                    client.Properties.Add(property.AddToDomain());
                }
            }

            return client;
        }

        public void AddFromDomain(Client client)
        {
            Title = client.Title;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Email = client.Email;
            Phone = client.Phone;
            ClientType = client.ClientType;
        }
    }
}

