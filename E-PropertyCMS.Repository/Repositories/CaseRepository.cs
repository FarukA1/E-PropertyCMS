using System;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Model;
using E_PropertyCMS.Repository.Contexts;
using E_PropertyCMS.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PropertyCMS.Repository.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        //private readonly ICaseContext _caseContext;
        private readonly ICoreContext _coreContext;

        public CaseRepository(ICoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<List<Case>> GetCases()
        {
            {
                var cases = new List<Case>();

                var casesDbModel = await _coreContext.Case
                     .Include(v => v.Property)
                     .Include(v => v.Client)
                    .ToListAsync();

                foreach (var caseDbModel in casesDbModel)
                {
                    cases.Add(caseDbModel.AddToDomain());
                }

                return cases;
            }

        }

        public async Task<Case> GetCaseById(Guid Id)
        {
            var kase = await _coreContext.Case
                     .Include(v => v.Property)
                     .Include(v => v.Client)
                .FirstOrDefaultAsync(v => v.Key == Id);

            if (kase == null)
            {
                return null;
            }

            return kase.AddToDomain();
        }

        public async Task<List<Case>> GetCasesByClientId(Guid clientId)
        {
            var cases = new List<Case>();

            var casesDbModel = await _coreContext.Case
                .Include(v => v.Client)
                .Where(x => x.Client.Key == clientId)
                .ToListAsync();

            if(casesDbModel != null)
            {
                foreach (var caseDbModel in casesDbModel)
                {
                    cases.Add(caseDbModel.AddToDomain());
                }
            }

            return cases;
        }

        public async Task<List<Case>> GetCasesByType(string caseType)
        {
            var cases = new List<Case>();

            var casesDbModel = await _coreContext.Case
                .Where(x => x.CaseType.Type == caseType)
                .ToListAsync();

            if (casesDbModel != null)
            {
                foreach (var caseDbModel in casesDbModel)
                {
                    cases.Add(caseDbModel.AddToDomain());
                }
            }

            return cases;
        }

        public async Task<List<Case>> GetCasesByStatus(string caseStatus)
        {
            var cases = new List<Case>();

            var casesDbModel = await _coreContext.Case
                .Where(x => x.CaseStatus.Status == caseStatus)
                .ToListAsync();

            if (casesDbModel != null)
            {
                foreach (var caseDbModel in casesDbModel)
                {
                    cases.Add(caseDbModel.AddToDomain());
                }
            }

            return cases;
        }

        public async Task<Case> StoreCase(Case kase)
        {
            var exist = await _coreContext.Case
                .Include(v => v.Client)
                .Include(v => v.Property)
                .FirstOrDefaultAsync(v => v.Key == kase.Id);

            if(exist == null)
            {
                exist = new CaseDbModel()
                {
                    Key = kase.Id
                };
                _coreContext.Case.Add(exist);
            }
            exist.AddFromDomain(kase);


            return exist.AddToDomain();
        }
    }
}

