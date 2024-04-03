using System;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Domain.Model;
namespace E_PropertyCMS.Core.Services
{
	public interface ICaseService
	{
		Task<List<Case>> GetCases();
		Task<Case> GetCaseById(Guid caseId);
		Task<Case> StoreCase(CaseDto dto);
		Task<CaseType> StoreCaseType(CaseTypeDto dto);
		Task<List<Case>> Search(string searchQuery);
	}
}

