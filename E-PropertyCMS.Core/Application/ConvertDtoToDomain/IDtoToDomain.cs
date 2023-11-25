using System;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Application.ConvertDtoToDomain
{
	public interface IDtoToDomain
	{
        Task<Client> GetClient(ClientDto dto);
        Task<Property> GetProperty(PropertyDto dto);
        Task<Address> GetAddress(AddressDto dto);
        Task<Room> GetRoom(RoomDto dto);
    }
}

