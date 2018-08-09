using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSecondLevelCache.Redis
{
    public class RedisCacheStore : ICacheStore
    {
        private readonly IDatabase _database;
        private readonly ISerializer _serializer;
        private readonly CommandFlags _readFlag;

        public RedisCacheStore(IRedisConnectionWrapper connectionWrapper, IRedisServerSettings settings, ISerializer serializer)
        {
            _database = connectionWrapper.Database(settings.DefaultDb);
            _serializer = serializer;
            _readFlag = settings.PreferSlaveForRead ? CommandFlags.PreferSlave : CommandFlags.PreferMaster;
        }

        async Task<bool> ICacheStore.ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        async Task<T> ICacheStore.GetAsync<T>(string key)
        {
            var result = await _database.StringGetAsync(key, _readFlag);

            return _serializer.Deserialize<T>(result);
        }

        async Task ICacheStore.SetAsync<T>(string key, T value, TimeSpan expiredIn)
        {
            await _database.StringSetAsync(key, _serializer.Serialize(value), expiredIn);
        }

        async Task ICacheStore.RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        bool ICacheStore.Exists(string key)
        {
            return _database.KeyExists(key, _readFlag);
        }

        T ICacheStore.Get<T>(string key)
        {
            return _serializer.Deserialize<T>(_database.StringGet(key, CommandFlags.PreferSlave));
        }

        void ICacheStore.Set<T>(string key, T value, TimeSpan expiredIn)
        {
            _database.StringSet(key, _serializer.Serialize(value), expiredIn);
        }

        void ICacheStore.Remove(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
