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
        private readonly IClientRepository _clientRepository ;
        private readonly IPropertyRepository _prropertyRepository ;

		private string caseCacheKey = "cases";

        public CaseService(IMemoryCache memoryCache, IDtoToDomain dtoToDomain, ICaseRepository caseRepository,IClientRepository clientRepository,
        IPropertyRepository prropertyRepository)
		{
			_memoryCache = memoryCache;
            _dtoToDomain = dtoToDomain;
			_caseRepository = caseRepository;
            _clientRepository = clientRepository;
            _prropertyRepository = prropertyRepository;
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

        public async Task<Case> GetCaseById(Guid caseId)
		{
			var kase = await _caseRepository.GetCaseById(caseId);

			return kase;
		}

        public async Task<List<Case>> Search(string searchQuery)
        {

            var cases = await _caseRepository.Search(searchQuery);

            if (cases == null)
            {
                throw new EPropertyCMSException();
            }

            return cases;
        }

		public async Task<Case> StoreCase(CaseDto dto)
		{
            var kase = await _dtoToDomain.GetCase(dto);

            var caseType = await GetCaseTypeById(dto.CaseTypeId);
            
            if(caseType == null) 
            {
                throw new EPropertyCMSException("Case type does not exist");
            }

            kase.CaseType = caseType;

            var client = await _clientRepository.GetClientById(dto.ClientId);

            if(client == null) 
            {
                throw new EPropertyCMSException("Client type does not exist");
            }

            kase.Client = client;

            var property = await _prropertyRepository.GetPropertyById(dto.PropertyId);

            if(property == null) 
            {
                throw new EPropertyCMSException("Property type does not exist");
            }

            kase.Property = property;

			await _caseRepository.StoreCase(kase);

            return kase;

        }


        public async Task<CaseType> GetCaseTypeById(Guid caseTypeId)
		{
			var type = await _caseRepository.GetCaseTypeById(caseTypeId);

			return type;
		}

        public async Task<CaseType> StoreCaseType(CaseTypeDto dto)
		{
            var caseType = await _dtoToDomain.GetCaseType(dto);

			await _caseRepository.StoreCaseType(caseType);

            return caseType;

        }
	}
}

