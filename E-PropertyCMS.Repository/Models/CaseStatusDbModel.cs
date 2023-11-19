using System;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Repository.Models
{
	public class CaseStatusDbModel
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Status { get; set; }

        public CaseStatus AddToDomain()
        {
            var caseStatus = new CaseStatus()
            {
                Id = Key,
                Status = Status
            };

            return caseStatus;
        }

        public void AddFromDomain(CaseStatus caseStatus)
        {
            Status = caseStatus.Status;
        }
    }
}

