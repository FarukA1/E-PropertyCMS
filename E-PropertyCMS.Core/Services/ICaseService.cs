using System;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Domain.Model;
namespace E_PropertyCMS.Core.Services
{
	public interface ICaseService
	{
		Task<List<Case>> GetCases();
		Task<Case> GetCasesById(Guid caseId);
		Task<Case> StoreCase(CaseDto dto);
	}
}

