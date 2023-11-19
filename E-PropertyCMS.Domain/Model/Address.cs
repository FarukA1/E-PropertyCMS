using System;
namespace E_PropertyCMS.Domain.Model
{
	public class Address
	{
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}

