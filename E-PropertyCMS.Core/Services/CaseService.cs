using System;
using E_PropertyCMS.Core.Repositories;

namespace E_PropertyCMS.Core.Services
{
	public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;

        public CaseService(ICaseRepository caseRepository)
		{
			_caseRepository = caseRepository;
		}
	}
}

