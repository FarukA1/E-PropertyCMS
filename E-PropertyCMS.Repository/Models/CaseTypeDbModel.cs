using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class CaseTypeDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Type { get; set; }

        public CaseType AddToDomain()
        {
            var caseType = new CaseType()
            {
                Id = Key,
                Type = Type
            };

            return caseType;
        }

        public void AddFromDomain(CaseType caseType)
        {
            Type = caseType.Type;
        }
    }
}

