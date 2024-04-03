using System;
using System.Collections.Generic;
using System.Net;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Core.Services;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Repositories
{
	public class ClientRepository : IClientRepository
    {
        private readonly ICoreContext _coreContext;

		public ClientRepository(ICoreContext coreContext)
		{
            _coreContext = coreContext;
		}

        public async Task<List<Client>> GetClients()
        {
            var clients = new List<Client>();

            var clientsDbModel = await _coreContext.Client
                 .Include(v => v.Address)
                .ToListAsync();

            foreach (var clientDbModel in clientsDbModel)
            {
                clients.Add(clientDbModel.AddToDomain());
            }

            return clients;
        }

        public async Task<List<Client>> Search(string searchQuery)
        {
            var clients = new List<Client>();

            searchQuery = searchQuery.ToLower();

            var clientsDbModel = await _coreContext.Client
                 .Include(v => v.Address)
                 .Where(v => (searchQuery.Length == 1 && (v.FirstName.ToLower().StartsWith(searchQuery) || v.LastName.ToLower().StartsWith(searchQuery))) ||
                    (searchQuery.Length > 1 && (v.FirstName.ToLower().Equals(searchQuery) || v.LastName.ToLower().Equals(searchQuery))))
                .ToListAsync();

            foreach (var clientDbModel in clientsDbModel)
            {
                clients.Add(clientDbModel.AddToDomain());
            }

            return clients;
        }

        public async Task<Client> GetClientById(Guid Id)
		{
			var client = await _coreContext.Client
                .Where(v => v.Key == Id)
                .Include(v => v.Address)
                .Include(v => v.Properties)
                   .ThenInclude(x => x.Address)
                .Include(v => v.Properties)
                   .ThenInclude(y => y.Rooms)
                .FirstOrDefaultAsync();

            return client?.AddToDomain();
		}

        public async Task<List<Property>> GetClientProperties(Guid clientId)
        {
            var propertyList = new List<Property>();

            var properties = await _coreContext.Property
                .Include(v => v.Client)
                .Where(v => v.Client.Key == clientId)
                .Include(v => v.Address)
                .Include(v => v.Rooms)
                .ToListAsync();

            if (!properties.Any())
            {
                return null;
            }

            foreach(var propertydb in properties)
            {
                var propertydomain = propertydb.AddToDomain();

                propertyList.Add(propertydomain);
            }

            return propertyList;
        }

        public async Task<List<Case>> GetClientCases(Guid clientId)
        {
            var caseList = new List<Case>();

            var cases = await _coreContext.Case
                .Include(v => v.Client)
                .Where(v => v.Client.Key == clientId)
                .ToListAsync();

            if (!cases.Any())
            {
                return null;
            }

            foreach(var casedb in cases)
            {
                var casedomain = casedb.AddToDomain();

                caseList.Add(casedomain);
            }

            return caseList;
        }


        public async Task<Client> StoreClient(Client client)
		{
			var exist = await _coreContext.Client
				.Include(v => v.Address)
				.FirstOrDefaultAsync(v => v.Key == client.Id);

			if(exist == null)
			{
				exist = new ClientDbModel()
				{
					Key = client.Id,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
				};

                _coreContext.Client.Add(exist);
            }

            var clientAddressExist = await AddAddress(client.Address);

            exist.Address = clientAddressExist;
            exist.AddressId = clientAddressExist.Id;

            if(client.Properties.Any())
            {
                foreach(var property in client.Properties)
                {
                    var propertyExist = await _coreContext.Property.FirstOrDefaultAsync(v => v.Key == property.Id);

                    if(propertyExist == null)
                    {
                        propertyExist = new PropertyDbModel()
                        {
                            Key = property.Id,
                            Client = exist,
                            clientId = exist.Id
                        };
                    }

                    var propertyAddressExist = await AddAddress(property.Address);

                    propertyExist.Address = propertyAddressExist;
                    propertyExist.AddressId = propertyAddressExist.Id;

                    foreach (var room in property.Rooms)
                    {
                        var roomExist = await _coreContext.Room.FirstOrDefaultAsync(v => v.Key == room.Id);

                        if(roomExist == null)
                        {
                            roomExist = new RoomDbModel()
                            {
                                Key = room.Id,
                                Property = propertyExist,
                                PropertyId = propertyExist.Id

                            };
                        }

                        _coreContext.Room.Add(roomExist);
                        roomExist.AddFromDomain(room);

                        propertyExist.Rooms.Add(roomExist);
                    }

                    _coreContext.Property.Add(propertyExist);
                    propertyExist.AddFromDomain(property);

                    exist.Properties.Add(propertyExist);
                }
            }

            exist.AddFromDomain(client);
            await _coreContext.SaveChangesAsync();

            return exist?.AddToDomain();
		}

        public async Task<Property> GetPropertyById(Guid Id)
        {

            var property = await _coreContext.Property
                .Include(v => v.Address)
                .Include(v => v.Rooms)
                .FirstOrDefaultAsync(v => v.Key == Id);

			if(property == null)
			{
				return null;
			}

            return property?.AddToDomain();
        }

        public async Task<Property> StoreProperty(Guid clientId, Property property)
		{
			var client = await _coreContext.Client.
				Where(v => v.Key == clientId).FirstOrDefaultAsync();

			if(client == null)
			{
				return null;
			}

			var exist = await _coreContext.Property
				.Where(v => v.Key == property.Id)
				.FirstOrDefaultAsync();

			if(exist == null)
			{
				exist = new PropertyDbModel()
				{
					Key = property.Id,
					Client = client,
					clientId = client.Id,
				};

                _coreContext.Property.Add(exist);
            }


            exist.AddFromDomain(property);

            return exist?.AddToDomain();
        }

        public async Task<Address> GetClientAddress(Guid clientId, Guid addressId)
        {
			var client = await GetClientById(clientId);

			if(client == null)
			{
				return null;
            }

            var property = await _coreContext.Address
                .FirstOrDefaultAsync(v => v.Key == addressId);

            return property?.AddToDomain();
        }

        public async Task<AddressDbModel> AddAddress(Address address)
        {
            var exist = await _coreContext.Address.FirstOrDefaultAsync(v => v.Key == address.Id);

            if (exist == null)
            {
                exist = new AddressDbModel()
                {
                    Key = address.Id
                };
                _coreContext.Address.Add(exist);
            }

            exist.AddFromDomain(address);

            return exist;
        }

        public async Task<Address> StoreAddress(Address address)
        {
            var exist = await _coreContext.Address.FirstOrDefaultAsync(v => v.Key == address.Id);

            if(exist == null)
            {
                exist = new AddressDbModel()
                {
                    Key = address.Id
                };
                _coreContext.Address.Add(exist);
            }

            exist.AddFromDomain(address);

            return exist?.AddToDomain();
        }

        public async Task<List<Room>> GetRooms(Guid propertyId)
        {
            var property = await _coreContext.Property.Where(v => v.Key == propertyId).FirstOrDefaultAsync();

			if(property == null)
			{
				return null;
			}

            var rooms = new List<Room>();

            var roomsDbModel = await _coreContext.Room.Where(v => v.PropertyId == property.Id)
                .ToListAsync();

            foreach (var roomDbModel in roomsDbModel)
            {
                rooms.Add(roomDbModel.AddToDomain());
            }

            return rooms;
        }

        public async Task<Room> StoreRoom(Guid propertyId, Room room)
        {
            var property = await _coreContext.Property.FirstOrDefaultAsync(v => v.Key == propertyId);

            if(property == null)
            {
                throw new EPropertyCMSException("Cannot load load property");
            }

            var exist = await _coreContext.Room.FirstOrDefaultAsync(v => v.Key == room.Id);

            if(exist == null)
            {
                exist = new RoomDbModel()
                {
                    Key = room.Id,
                    PropertyId = property.Id,
                    Property = property
                };

                _coreContext.Room.Add(exist);
            }

            exist.AddFromDomain(room);

            return exist?.AddToDomain();
        }
    }
}

