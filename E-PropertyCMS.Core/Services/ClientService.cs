using System;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_clientRepository = clientRepository;
		}

		public async Task<List<Client>> GetClients()
		{
			var clients = await _clientRepository.GetClients();

			if(clients == null)
			{
                throw new EPropertyCMSException();
            }

			return clients;
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

        public async Task<List<Client>> GetClientsByType(ClientType clientType)
        {
            var client = await _clientRepository.GetClientsByType(clientType);

            if (client == null)
            {
                throw new EPropertyCMSException();
            }

            return client;
        }

        public async Task<List<Client>> GetClientsByType(ClientType clientType, PaginationFilter filter)
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

			if(client == null)
			{
				throw new EPropertyCMSException();
			}

			return client;
		}

		public async Task<Client> StoreClient(ClientDto dto)
		{
			var client = new Client()
			{
				Id = Guid.NewGuid()
			};

			if(dto.FirstName != null)
			{
				client.FirstName = dto.FirstName;
			}

			if(dto.LastName != null)
			{
				client.LastName = dto.LastName;
			}

            if (dto.Phone != null)
            {
                client.Phone = dto.Phone;
            }

			if (dto.ClientType != null)
			{
				client.ClientType = dto.ClientType;
			}

			await _clientRepository.StoreClient(client);

			if(dto.Address != null)
			{
				client.Address = new Address()
				{
					Id = Guid.NewGuid()
				};

                if (dto.Address.Number != null)
				{
                    client.Address.Number = dto.Address.Number;
                }

                if (dto.Address.StreetName != null)
                {
                    client.Address.StreetName = dto.Address.StreetName;
                }

                if (dto.Address.City != null)
                {
                    client.Address.City = dto.Address.City;
                }

                if (dto.Address.Number != null)
                {
                    client.Address.PostCode = dto.Address.PostCode;
                }

                if (dto.Address.Number != null)
                {
                    client.Address.Country = dto.Address.Country;
                }

                await _clientRepository.StoreClientAddress(client.Id, client.Address);
            }

            if (!dto.Properties.Any())
            {
                var properties = new List<Property>();

                foreach (var dtoProperty in dto.Properties)
                {
                    var property = await StoreProperty(client.Id, dtoProperty);

                    properties.Add(property);
                }

                client.Properties = properties;
            }

            _unitOfWork.SaveChangesAsync();

            return client;

        }

        public async Task<Property> StoreProperty(Guid Id, PropertyDto dto)
        {
            return null;
        }
    }
}

