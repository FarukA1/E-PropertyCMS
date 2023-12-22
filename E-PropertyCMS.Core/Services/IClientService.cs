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
        Task<List<Client>> Search(string searchQuery);
        Task<Client> GetClientById(Guid clientId);
        Task<List<Property>> GetClientProperties(Guid clientId);
        Task<Client> StoreClient(ClientDto dto);
    }
}

