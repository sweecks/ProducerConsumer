using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webapi.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;
        public RedisService(IDistributedCache cache)
        {
            this._cache = cache;
        }

        public async Task AddList<T>(string key, IEnumerable<T> list)
        {
            var serializedProductsList = JsonConvert.SerializeObject(list);
            var redisProductsList = Encoding.UTF8.GetBytes(serializedProductsList);
            var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await this._cache.SetAsync(key, redisProductsList, options);
        }
    }
}
