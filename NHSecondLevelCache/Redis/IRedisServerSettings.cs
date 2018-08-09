namespace NHSecondLevelCache.Redis
{
    public interface IRedisServerSettings
    {
        bool PreferSlaveForRead { get; }
        string ConnectionStringOrName { get; }
        int DefaultDb { get; }
    }
}
