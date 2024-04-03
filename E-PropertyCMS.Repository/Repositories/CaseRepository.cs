using System;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Enumeration;
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
                     .Include(v => v.CaseType)
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
                     .Include(v => v.CaseType)
                     .Include(v => v.Property)
                     .Include(v => v.Client)
                .FirstOrDefaultAsync(v => v.Key == Id);

            if (kase == null)
            {
                return null;
            }

            return kase.AddToDomain();
        }

        public async Task<List<Case>> Search(string searchQuery)
        {
            var cases = new List<Case>();

            searchQuery = searchQuery.ToLower();

            var casesDbModel = await _coreContext.Case
                     .Include(v => v.CaseType)
                     .Include(v => v.Property)
                     .Include(v => v.Client)
                 .Where(v => (searchQuery.Length == 1 && v.Reference.ToLower().StartsWith(searchQuery)) ||
                    searchQuery.Length > 1 && v.Reference.ToLower().Equals(searchQuery))
                .ToListAsync();

            foreach (var caseDbModel in casesDbModel)
            {
                cases.Add(caseDbModel.AddToDomain());
            }

            return cases;
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

        public async Task<List<Case>> GetCasesByStatus(CaseStatus caseStatus)
        {
            var cases = new List<Case>();

            var casesDbModel = await _coreContext.Case
                .Where(x => x.CaseStatus == caseStatus)
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
                    Key = kase.Id,
                    Reference = kase.Reference,
                    CaseStatus = CaseStatus.New,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                };

                var caseTypeExist = await _coreContext.CaseType.FirstOrDefaultAsync(v => v.Key == kase.CaseType.Id);

                exist.CaseType = caseTypeExist;
                exist.CaseTypeId = caseTypeExist.Id;

                var client = await _coreContext.Client.FirstOrDefaultAsync(v => v.Key == kase.Client.Id);

                exist.Client = client;
                exist.ClientId = client.Id;

                var property = await _coreContext.Property.FirstOrDefaultAsync(v => v.Key == kase.Property.Id);

                exist.Property = property;
                exist.PropertyId = property.Id;
                

                _coreContext.Case.Add(exist);
            }
            exist.AddFromDomain(kase);

            await _coreContext.SaveChangesAsync();
            return exist.AddToDomain();
        }

        public async Task<CaseType> GetCaseTypeById(Guid Id)
        {
            var type = await _coreContext.CaseType
                .FirstOrDefaultAsync(v => v.Key == Id);

            if (type == null)
            {
                return null;
            }

            return type.AddToDomain();
        }

        public async Task<CaseType> StoreCaseType(CaseType caseType)
        {
            var exist = await _coreContext.CaseType
                .FirstOrDefaultAsync(v => v.Key == caseType.Id);

            if(exist == null)
            {
                exist = new CaseTypeDbModel()
                {
                    Key = caseType.Id
                };
                _coreContext.CaseType.Add(exist);
            }
            exist.AddFromDomain(caseType);

            await _coreContext.SaveChangesAsync();
            return exist.AddToDomain();
        }

        public async Task<CaseType> AddCaseType(CaseType caseType)
        {
            var exist = await _coreContext.CaseType.FirstOrDefaultAsync(v => v.Key == caseType.Id);

            if (exist == null)
            {
                exist = new CaseTypeDbModel()
                {
                    Key = caseType.Id
                };
                _coreContext.CaseType.Add(exist);
            }

            exist.AddFromDomain(caseType);

            return caseType;
        }
    }
}

