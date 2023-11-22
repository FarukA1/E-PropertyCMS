using System;
using System.Threading.Tasks;
using E_PropertyCMS.Domain.Model;

namespace E_PropertyCMS.Core.Repositories
{
	public interface ICaseRepository
	{
        Task<List<Case>> GetCases();
        Task<Case> GetCaseById(Guid Id);
        Task<List<Case>> GetCasesByType(string caseType);
        Task<List<Case>> GetCasesByStatus(string caseStatus);
        Task<Case> StoreCase(Case kase);
    }
}

