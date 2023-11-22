using System;
namespace E_PropertyCMS.Domain.Model
{
	public class Case
	{
        public Guid Id { get; set; }
        public CaseType CaseType { get; set; }
        public CaseStatus CaseStatus { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}

