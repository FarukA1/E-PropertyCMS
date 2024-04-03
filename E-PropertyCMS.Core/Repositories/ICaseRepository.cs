using System;
using System.Threading.Tasks;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Domain.Enumeration;

namespace E_PropertyCMS.Core.Repositories
{
	public interface ICaseRepository
	{
        Task<List<Case>> GetCases();
        Task<Case> GetCaseById(Guid Id);
        Task<List<Case>> GetCasesByType(string caseType);
        Task<List<Case>> GetCasesByStatus(CaseStatus caseStatus);
        Task<List<Case>> Search(string searchQuery);
        Task<Case> StoreCase(Case kase);
        Task<CaseType> GetCaseTypeById(Guid Id);
        Task<CaseType> StoreCaseType(CaseType caseType);
    }
}

