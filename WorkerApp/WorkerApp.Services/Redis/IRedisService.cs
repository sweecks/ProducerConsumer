using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerApp.Services.Redis
{
    public interface IRedisService
    {
        public string GetSerializedList(string key);
    }
}
