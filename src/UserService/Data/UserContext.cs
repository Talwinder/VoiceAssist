using UserService.Data.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Data
{
    public class  UserContext : IUserContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public UserContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}