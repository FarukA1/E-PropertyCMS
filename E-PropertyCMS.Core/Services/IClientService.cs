using System;
using E_PropertyCMS.Core.Application.Dto;
using System.Threading.Tasks;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Domain.Filter;

namespace E_PropertyCMS.Core.Services
{
	public interface IClientService
	{
        Task<List<Client>> GetClients();
        Task<List<Client>> GetClients(PaginationFilter filter);
        Task<List<Client>> GetClientsByType(ClientType clientType);
        Task<List<Client>> GetClientsByType(ClientType clientType, PaginationFilter filter);
        Task<Client> GetClientById(Guid clientId);
        Task<Client> StoreClient(ClientDto dto);
        Task<Property> StoreProperty(Guid Id, PropertyDto dto);
    }
}

