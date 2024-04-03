using System;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Domain.Utilities;

namespace E_PropertyCMS.Core.Application.ConvertDtoToDomain
{
	public class DtoToDomain : IDtoToDomain
    {
		public DtoToDomain()
		{
		}

        public async Task<Client> GetClient (ClientDto dto)
        {
            var client = new Client()
            {
                Id = Guid.NewGuid()
            };

            if (dto.Title != null)
            {
                client.Title = dto.Title;
            }

            if (dto.FirstName != null)
            {
                client.FirstName = dto.FirstName;
            }

            if (dto.LastName != null)
            {
                client.LastName = dto.LastName;
            }

            if (dto.Phone != null)
            {
                client.Phone = dto.Phone;
            }

            if (dto.Email != null)
            {
                client.Email = dto.Email;
            }

            if (dto.ClientType != null)
            {
                client.ClientType = dto.ClientType;
            }

            return client;
        }

        public async Task<Case> GetCase(CaseDto dto)
        {
            var kase = new Case()
            {
                Id = Guid.NewGuid(),
            };

            string reference = RandomValue.RandomString(8);

            kase.Reference = reference;

            return kase;
        }

        public async Task<CaseType> GetCaseType(CaseTypeDto dto)
        {
            var caseType = new CaseType()
            {
                Id = Guid.NewGuid()
            };

            if (dto.Type != null)
            {
                caseType.Type = dto.Type;
            }

            return caseType;
        }

        public async Task<Property> GetProperty(PropertyDto dto)
        {
            var property = new Property()
            {
                Id = Guid.NewGuid()
            };

            if (dto.Price != null)
            {
                property.Price = dto.Price;
            }

            if (dto.PropertyType != null)
            {
                property.PropertyType = dto.PropertyType;
            }

            if (dto.Description != null)
            {
                property.Description = dto.Description;
            }

            if (dto.PropertyStatus != null)
            {
                property.PropertyStatus = dto.PropertyStatus;
            }

            return property;
        }

        public async Task<Address> GetAddress(AddressDto dto)
        {
            var address = new Address()
            {
                Id = Guid.NewGuid()
            };

            if (dto.Number != null)
            {
                address.Number = dto.Number;
            }

            if (dto.StreetName != null)
            {
                address.StreetName = dto.StreetName;
            }

            if (dto.City != null)
            {
                address.City = dto.City;
            }

            if (dto.Number != null)
            {
                address.PostCode = dto.PostCode;
            }

            if (dto.Number != null)
            {
                address.Country = dto.Country;
            }

            return address;
        }

        public async Task<Room> GetRoom(RoomDto dto)
        {
            var room = new Room()
            {
                Id = Guid.NewGuid()
            };

            if (dto.RoomType != null)
            {
                room.RoomType = dto.RoomType;
            }

            if (dto.Description != null)
            {
                room.Description = dto.Description;
            }

            return room;
        }

        public async Task<User> GetUser(UserDto dto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid()
            };

            if(dto.UniqueId == null)
            {
                throw new EPropertyCMSException("A unique Id is needed to create a user. Check with you admin."); 
            }

            user.UniqueId = dto.UniqueId;

            if (dto.Title != null)
            {
                user.Title = dto.Title;
            }

            if (dto.FirstName != null)
            {
                user.FirstName = dto.FirstName;
            }

            if (dto.LastName != null)
            {
                user.LastName = dto.LastName;
            }

            if (dto.UserName != null)
            {
                user.UserName = dto.UserName;
            }

            if (dto.Phone != null)
            {
                user.Phone = dto.Phone;
            }

            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }

            user.Picture = dto.Picture;

            return user;
        }
    }
}

