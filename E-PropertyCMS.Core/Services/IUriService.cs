using System;
using E_PropertyCMS.Domain.Filter;

namespace E_PropertyCMS.Core.Services
{
	public interface IUriService
	{
        Uri GetPageUri(PaginationFilter paginationFilter, string route);
    }
}

