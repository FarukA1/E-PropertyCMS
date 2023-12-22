using System;
using E_PropertyCMS.Core.Application.ConvertDtoToDomain;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.Caching;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;
using Microsoft.Extensions.Caching.Memory;

namespace E_PropertyCMS.Core.Services
{
	public class ClientService : IClientService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDtoToDomain _dtoToDomain;
        private readonly IClientRepository _clientRepository;

        private string clientCacheKey = "clients";

        public ClientService(IMemoryCache memoryCache, IDtoToDomain dtoToDomain, IClientRepository clientRepository)
		{
            _memoryCache = memoryCache;
            _dtoToDomain = dtoToDomain;
			_clientRepository = clientRepository;
		}

        public async Task<List<Client>> GetClients()
        {
            
            var cacheService = new CacheService<Client>(_memoryCache, clientCacheKey);

            var clients = await cacheService.GetCacheData();

            if(clients == null)
            {
                clients = await _clientRepository.GetClients();

                await cacheService.StoreCacheData(clients);
            }

            if (clients == null)
            {
                throw new EPropertyCMSException();
            }

            return clients;
        }

        public async Task<List<Client>> Search(string searchQuery)
        {

            var clients = await _clientRepository.Search(searchQuery);

            if (clients == null)
            {
                throw new EPropertyCMSException();
            }

            return clients;
        }

        public async Task<Client> GetClientById(Guid clientId)
		{
			var client = await _clientRepository.GetClientById(clientId);

			return client;
		}

        public async Task<List<Property>> GetClientProperties(Guid clientId)
        {
            var client = await GetClientById(clientId);

            if(client == null)
            {
                throw new EPropertyCMSException($"Client {clientId} does exist");
            }

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

