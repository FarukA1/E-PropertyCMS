using System;
using Microsoft.AspNetCore.WebUtilities;
using E_PropertyCMS.Domain.Filter;

namespace E_PropertyCMS.Core.Services
{
	public class UriService : IUriService
    {
		private readonly string _baseUri;

		public UriService(string baseUri)
		{
			_baseUri = baseUri;
		}

		public Uri GetPageUri(PaginationFilter paginationFilter, string route)
		{
			var _enpointUri = new Uri(string.Concat(_baseUri, route));
			var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "PageNumber", paginationFilter.PageNumber.ToString());
			modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", paginationFilter.PageSize.ToString());
			return new Uri(modifiedUri);
		}
	}
}

