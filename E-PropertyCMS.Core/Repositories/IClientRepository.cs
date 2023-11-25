﻿using System;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Filter;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Repositories
{
	public interface IClientRepository
	{
        Task<List<Client>> GetClients();
        Task<List<Client>> GetClients(PaginationFilter filter);
        Task<List<Client>> GetClientsByType(ClientType? clientType);
        Task<List<Client>> GetClientsByType(ClientType? clientType, PaginationFilter filter);
        Task<Client> GetClientById(Guid Id);
        Task<List<Property>> GetClientProperties(Guid clientId);
        Task<Client> StoreClient(Client client);
        Task<Property> GetPropertyById(Guid Id);
        Task<Property> StoreProperty(Guid clientId, Property property);
        Task<Address> GetClientAddress(Guid clientId, Guid addressId);
        Task<Address> StoreAddress(Address address);
        Task<List<Room>> GetRooms(Guid propertyId);
    }
}

