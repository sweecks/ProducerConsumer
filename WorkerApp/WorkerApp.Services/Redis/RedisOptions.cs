using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerApp.Services.Redis
{
    public class RedisOptions : IOptions<RedisCacheOptions>
    {
        public RedisCacheOptions Value => new RedisCacheOptions
        {
            Configuration = "localhost:6379"
        };
    }
}
