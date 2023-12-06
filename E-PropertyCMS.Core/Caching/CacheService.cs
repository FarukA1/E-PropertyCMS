using System;
using Microsoft.Extensions.Caching.Memory;

namespace E_PropertyCMS.Core.Caching
{
	public class CacheService<T>
    {
        private readonly IMemoryCache _memoryCache;
		private string _Key;

        public CacheService(IMemoryCache memoryCache, string Key)
		{
			_memoryCache = memoryCache;
			_Key = Key;
		}

		public async Task<List<T>> GetCacheData()
		{
			if(_memoryCache.TryGetValue(_Key, out List<T> data))
			{
				return data;
			}

			return null;
        }

		public async Task StoreCacheData(List<T> _newData)
		{
			_memoryCache.Set(_Key, _newData,
				new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));
        }
	}
}

