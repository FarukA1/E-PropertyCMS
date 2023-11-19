using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class CaseDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public CaseTypeDbModel CaseType { get; set; }
        public List<ClientDbModel> Clients { get; set; }
        public List<PropertyDbModel> Properties { get; set; }
        public CaseStatusDbModel CaseStatus { get; set; }

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

            foreach(var client in Clients)
            {
                kase.Clients.Add(client.AddToDomain());
            }

            foreach (var property in Properties)
            {
                kase.Properties.Add(property.AddToDomain());
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

