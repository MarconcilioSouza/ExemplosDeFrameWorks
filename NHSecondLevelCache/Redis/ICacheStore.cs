using System;
using System.Threading.Tasks;

namespace NHSecondLevelCache.Redis
{
    public interface ICacheStore
    {
        bool Exists(string key);
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiredIn);
        void Remove(string key);
        Task<bool> ExistsAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiredIn);
        Task RemoveAsync(string key);
    }
}
