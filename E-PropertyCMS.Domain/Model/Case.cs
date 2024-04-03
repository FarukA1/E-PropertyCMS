using System;
using E_PropertyCMS.Domain.Enumeration;
namespace E_PropertyCMS.Domain.Model
{
	public class Case
	{
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public Property Property { get; set; }
        public string Reference { get; set; }
        public CaseType CaseType { get; set; }
        public CaseStatus CaseStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}

