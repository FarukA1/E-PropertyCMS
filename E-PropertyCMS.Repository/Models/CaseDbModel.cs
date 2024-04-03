using System;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Domain.Enumeration;

namespace E_PropertyCMS.Repository.Models
{
	public class CaseDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Reference { get; set; }
        public CaseTypeDbModel CaseType { get; set; }
        public int CaseTypeId { get; set; }
        public ClientDbModel Client { get; set; }
        public int ClientId { get; set; }
        public PropertyDbModel Property { get; set; }  
        public int? PropertyId { get; set; }
        public CaseStatus CaseStatus { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public Case AddToDomain()
        {
            var kase = new Case()
            {
                Id = Key,
                Reference = Reference,
                CaseStatus = CaseStatus,
                CreatedOn = CreatedOn,
                LastModifiedOn = LastModifiedOn
            };

            if(CaseType != null)
            {
                kase.CaseType = CaseType.AddToDomain();
            }

            if(Client != null)
            {
                kase.Client = Client.AddToDomain();
            }

            if(Property != null)
            {
                kase.Property = Property.AddToDomain();
            }

            return kase;
        }

        public void AddFromDomain(Case kase)
        {
            CreatedOn = kase.CreatedOn;
            LastModifiedOn = kase.LastModifiedOn;
        }
    }
}

