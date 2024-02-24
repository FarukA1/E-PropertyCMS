using System;
using E_PropertyCMS.Core.Application.ConvertDtoToDomain;
using E_PropertyCMS.Core.Application.Dto;
using E_PropertyCMS.Core.Caching;
using E_PropertyCMS.Core.CustomException;
using E_PropertyCMS.Core.Repositories;
using E_PropertyCMS.Domain.Model;
using Microsoft.Extensions.Caching.Memory;

namespace E_PropertyCMS.Core.Services
{
	public class CaseService : ICaseService
    {
		private readonly IMemoryCache _memoryCache;
		private readonly IDtoToDomain _dtoToDomain;
        private readonly ICaseRepository _caseRepository;

		private string caseCacheKey = "cases";

        public CaseService(IMemoryCache memoryCache, IDtoToDomain dtoToDomain, ICaseRepository caseRepository)
		{
			_memoryCache = memoryCache;
            _dtoToDomain = dtoToDomain;
			_caseRepository = caseRepository;
		}

		public async Task<List<Case>> GetCases()
        {
            
            var cacheService = new CacheService<Case>(_memoryCache, caseCacheKey);

            var cases = await cacheService.GetCacheData();

            if(cases == null)
            {
                cases = await _caseRepository.GetCases();

                await cacheService.StoreCacheData(cases);
            }

            if (cases == null)
            {
                throw new EPropertyCMSException();
            }

            return cases;
        }

        // public async Task<List<Case>> Search(string searchQuery)
        // {

        //     var cases = await _caseRepository.Search(searchQuery);

        //     if (cases == null)
        //     {
        //         throw new EPropertyCMSException();
        //     }

        //     return cases;
        // }

        public async Task<Case> GetCasesById(Guid caseId)
		{
			var kase = await _caseRepository.GetCaseById(caseId);

			return kase;
		}

		public async Task<Case> StoreCase(CaseDto dto)
		{
            var kase = await _dtoToDomain.GetCase(dto);

			await _caseRepository.StoreCase(kase);

            return kase;

        }
	}
}

