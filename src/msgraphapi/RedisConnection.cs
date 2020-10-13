using System;
using StackExchange.Redis;

namespace msgraphapi
{
    public class RedisConnection
    {
        ConnectionMultiplexer  redis = ConnectionMultiplexer.Connect($"{Environment.GetEnvironmentVariable("REDIS_HOST")}:{Environment.GetEnvironmentVariable("REDIS_PORT")?? "6379"}");

        public void Add(string key, string value)
        {
            IDatabase db = redis.GetDatabase();
            db.StringSet(key, value);
        }

        public RedisValue Get(string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.StringGet(key);
        }
    }
}
