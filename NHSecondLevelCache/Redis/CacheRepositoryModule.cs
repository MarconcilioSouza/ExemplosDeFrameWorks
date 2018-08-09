using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSecondLevelCache.Redis
{
    public class CacheRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRedisServerSettings>().ToMethod(x => RedisServerSettings.Settings.Value).InSingletonScope();
            Bind<IRedisConnectionWrapper>().To<RedisConnectionWrapper>().InSingletonScope();
            Bind<ISerializer>().To<JsonSerializer>().InSingletonScope();
            Bind<ICacheStore>().To<RedisCacheStore>();
        }
    }
}
