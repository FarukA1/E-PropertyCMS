using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class CaseDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public CaseTypeDbModel CaseType { get; set; }
        public int CaseTypeId { get; set; }
        public ClientDbModel Client { get; set; }
        public int ClientId { get; set; }
        public PropertyDbModel Property { get; set; }
        public int PropertyId { get; set; }
        public CaseStatusDbModel CaseStatus { get; set; }
        public int CaseStatusId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public Case AddToDomain()
        {
            var kase = new Case()
            {
                Id = Key,
                CaseType = CaseType.AddToDomain(),
                CaseStatus = CaseStatus.AddToDomain()
                //CreatedOn = DateTime.Now
            };

            return kase;
        }

        public void AddFromDomain(Case kase)
        {
            CreatedOn = kase.CreatedOn;
            LastModifiedOn = kase.LastModifiedOn;
        }
    }
}

