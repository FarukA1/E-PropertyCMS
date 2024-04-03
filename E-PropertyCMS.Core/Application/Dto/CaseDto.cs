using System;
using E_PropertyCMS.Domain.Enumeration;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Application.Dto
{
	public class CaseDto
	{
        public Guid CaseTypeId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PropertyId { get; set; }
    }
}

