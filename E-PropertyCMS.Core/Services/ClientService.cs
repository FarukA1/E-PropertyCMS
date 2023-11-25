using System;
using E_PropertyCMS.Core.Application.ConvertDtoToDomain;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Services
{
	public class ClientService : IClientService
    {
        private readonly IDtoToDomain _dtoToDomain;
        private readonly IClientRepository _clientRepository;

        public ClientService(IDtoToDomain dtoToDomain, IClientRepository clientRepository)
		{
            _dtoToDomain = dtoToDomain;
			_clientRepository = clientRepository;
		}

        public async Task<List<Client>> GetClients()
        {
            var clients = await _clientRepository.GetClients();

            if (clients == null)
            {
                throw new EPropertyCMSException();
            }

            return clients;
        }

        public async Task<int> ClientsTotal()
        {
            var clients = await GetClients();

            if (clients == null)
            {
                return 0;
            }

            return clients.Count();
        }

        public async Task<List<Client>> GetClients(PaginationFilter filter)
        {
            var clients = await _clientRepository.GetClients(filter);

            if (clients == null)
            {
                throw new EPropertyCMSException();
            }

            return clients;
        }

        public async Task<List<Client>> GetClientsByType(ClientType? clientType)
        {
            var client = await _clientRepository.GetClientsByType(clientType);

            if (client == null)
            {
                throw new EPropertyCMSException();
            }

            return client;
        }

        public async Task<int> ClientsTypeTotal(ClientType? clientType)
        {
            var clientsType = await GetClientsByType(clientType);

            if (clientsType == null)
            {
                return 0;
            }

            return clientsType.Count();
        }

        public async Task<List<Client>> GetClientsByType(ClientType? clientType, PaginationFilter filter)
        {
            var client = await _clientRepository.GetClientsByType(clientType, filter);

            if (client == null)
            {
                throw new EPropertyCMSException();
            }

            return client;
        }

        public async Task<Client> GetClientById(Guid clientId)
		{
			var client = await _clientRepository.GetClientById(clientId);

			return client;
		}

        public async Task<List<Property>> GetClientProperties(Guid clientId)
        {
            var properties = await _clientRepository.GetClientProperties(clientId);

            if (!properties.Any())
            {
                return null;
            }

            return properties;
        }


        public async Task<Client> StoreClient(ClientDto dto)
		{
            var client = await _dtoToDomain.GetClient(dto);

            if(dto.Address != null)
            {
                var address = await _dtoToDomain.GetAddress(dto.Address);

                client.Address = address;
            }

            if(dto.Properties.Any())
            {
                foreach(var dtoProperty in dto.Properties)
                {
                    var property = await _dtoToDomain.GetProperty(dtoProperty);

                    if (dtoProperty.Address != null)
                    {
                        var propertyAddress = await _dtoToDomain.GetAddress(dtoProperty.Address);

                        property.Address = propertyAddress;
                    }

                    if(dtoProperty.Rooms.Any())
                    {
                        foreach(var dtoRoom in dtoProperty.Rooms)
                        {
                            var room = await _dtoToDomain.GetRoom(dtoRoom);

                            property.Rooms.Add(room);
                        }
                    }

                    client.Properties.Add(property);
                }
            }

			await _clientRepository.StoreClient(client);

            return client;

        }

    }
}

