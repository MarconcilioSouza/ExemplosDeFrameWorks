using System;
using System.Configuration;

namespace NHSecondLevelCache.Redis
{
    public class RedisServerSettings : ConfigurationSection, IRedisServerSettings
    {
        public static Lazy<IRedisServerSettings> Settings = new Lazy<IRedisServerSettings>(() => ConfigurationManager.GetSection("Redis.ServerSettings") as RedisServerSettings);

        [ConfigurationProperty("PreferSlaveForRead", IsRequired = false, DefaultValue = false)]
        public bool PreferSlaveForRead { get { return Convert.ToBoolean(this["PreferSlaveForRead"]); } }

        [ConfigurationProperty("ConnectionStringOrName", IsRequired = true)]
        public string ConnectionStringOrName { get { return this["ConnectionStringOrName"] as string; } }

        [ConfigurationProperty("DefaultDb", IsRequired = false, DefaultValue = 0)]
        public int DefaultDb { get { return Convert.ToInt32(this["DefaultDb"]); } }
    }
}
