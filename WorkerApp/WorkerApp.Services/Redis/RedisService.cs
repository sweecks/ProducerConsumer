using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerApp.Models.DTOs;

namespace WorkerApp.Services.Redis
{
    public class RedisService : IRedisService
    {
        RedisCache cache = new RedisCache(new RedisOptions());

        public string GetSerializedList(string key)
        {
            var redisList = cache.Get(key);
            var serializedTestList = Encoding.UTF8.GetString(redisList);

            return serializedTestList;
        }
    }
}
