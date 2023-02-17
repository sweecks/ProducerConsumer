using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webapi.Services.Redis
{
    public interface IRedisService
    {
        public Task AddList<T>(string key, IEnumerable<T> list);
    }
}
