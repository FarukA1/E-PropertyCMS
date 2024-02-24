using System;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Application.Dto
{
	public class CaseDto
	{
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public CaseType CaseType { get; set; }
        public CaseStatus CaseStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}

