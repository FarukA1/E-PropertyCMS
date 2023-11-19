using System;
using System.Collections.Generic;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Repositories
{
	public class ClientRepository
	{
		private readonly IClientContext _clientContext;
        private readonly IPropertyContext _propertyContext;

		public ClientRepository(IClientContext clientContext, IPropertyContext propertyContext)
		{
			_clientContext = clientContext;
			_propertyContext = propertyContext;
		}

		public async Task<List<Client>> GetClients()
		{
			var clients = new List<Client>();

			var clientsDbModel = await _clientContext.Client
                 .Include(v => v.Address)
                .ToListAsync();

			foreach (var clientDbModel in clientsDbModel)
			{
				clients.Add(clientDbModel.AddToDomain());
			}

			return clients;
        }

		public async Task<List<Client>> GetClientsByType(ClientType clientType)
		{
            var clients = new List<Client>();

            var clientsDbModel = await _clientContext.Client
                 .Include(v => v.Address)
                .Where(v => v.ClientType == clientType)
				.ToListAsync();

            foreach (var clientDbModel in clientsDbModel)
            {
                clients.Add(clientDbModel.AddToDomain());
            }

            return clients;
        }

		public async Task<Client> GetClientById(Guid Id)
		{
			var client = await _clientContext.Client
				.Include(v => v.Address)
				.FirstOrDefaultAsync(v => v.Key == Id);
            return client.AddToDomain();
		}

		public async Task<Client> StroreClient(Client client)
		{
			var exist = await _clientContext.Client
				.Include(v => v.Address)
				.FirstOrDefaultAsync(v => v.Key == client.Id);

			if(exist == null)
			{
				exist = new ClientDbModel()
				{
					Key = client.Id,
				};
				_clientContext.Client.Add(exist);
            }

            exist.AddFromDomain(client);

			return exist.AddToDomain();
		}

        public async Task<Property> GetPropertyById(Guid Id)
        {
            var property = await _propertyContext.Property
                .Include(v => v.Address)
                .FirstOrDefaultAsync(v => v.Key == Id);
            return property.AddToDomain();
        }

        public async Task<Property> GetPropertyOwnerById(Guid clientId, Guid propertyId)
        {
            var property = await _propertyContext.Property
                .Include(v => v.Address)
				.Where(v => v.PropertyOwner.Key == clientId)
                .FirstOrDefaultAsync(v => v.Key == propertyId);

            return property.AddToDomain();
        }

        public async Task<Property> GetPropertyOccupantById(Guid clientId, Guid propertyId)
        {
            var property = await _propertyContext.Property
                .Include(v => v.Address)
                .Where(v => v.CurrentOccupant.Key == clientId)
                .FirstOrDefaultAsync(v => v.Key == propertyId);

            return property.AddToDomain();
        }

        public async Task<Property> StoreProperty(Guid clientId, Property property)
		{
			var client = await _clientContext.Client.
				Where(v => v.Key == clientId).FirstOrDefaultAsync();

			if(client == null)
			{
				return null;
			}

			var exist = await _propertyContext.Property
				.Where(v => v.Key == property.Id)
				.FirstOrDefaultAsync();

			if(exist == null)
			{
				exist = new PropertyDbModel()
				{
					Key = property.Id,
					PropertyOwner = client,
					PropertyOwnerId = client.Id,
					CurrentOccupant = null
				};

                _propertyContext.Property.Add(exist);
            }


            exist.AddFromDomain(property);

            return exist.AddToDomain();
        }


        public async Task<Address> StoreAddress(Guid clientId, Address address)
		{
            var exist = await _clientContext.Client.FirstOrDefaultAsync(v => v.Key == clientId);

			if(exist != null && exist.Address == null)
			{
				exist.Address = new AddressDbModel()
				{
					Key = address.Id
				};
			_clientContext.Address.Add(exist.Address);
			}

			exist.Address.AddFromDomain(address);

			return exist.Address.AddToDomain();
        }
	}
}

