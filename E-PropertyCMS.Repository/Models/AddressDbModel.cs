using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class AddressDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Number { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string? County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Address AddToDomain()
        {
            var address = new Address()
            {
                Id = Key,
                Number = Number,
                StreetName = StreetName,
                City = City,
                County = County,
                PostCode = PostCode,
                Country = Country
            };

            return address;
        }

        public void AddFromDomain(Address address)
        {
            Number = address.Number;
            StreetName = address.StreetName;
            City = address.City;
            County = address.County;
            PostCode = address.PostCode;
            Country = address.Country;
        }
    }
}

