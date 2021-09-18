using AccountService.Data.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Entities;
using AccountService.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace AccountService.Data
{
    public class AccountContext : IAccountContext
    {
       
        private readonly MongoClient _mongoClient;
        ILogger<AccountContext> _logger;

        public AccountContext(ConnectionMultiplexer redisConnection,IAccountDatabaseSettings settings, ILogger<AccountContext> logger)
        {
            try
            {
                Redis = redisConnection.GetDatabase();
            }
            catch(RedisConnectionException)
            {
               // redisConnection.Reconnect();
                _logger.LogWarning( "Redis Connection Failed . Trying reconnect");
                 Redis = redisConnection.GetDatabase();
            }
           
           
            _mongoClient = new MongoClient(settings.ConnectionString);
            var database  = _mongoClient.GetDatabase(settings.DatabaseName);
            BankDetails = database.GetCollection<BankDetail>(settings.CollectionName);
            AccountContextSeed.SeedData(BankDetails);
        }

        public IDatabase Redis { get; }

        public IMongoCollection<BankDetail> BankDetails { get; set;}
    }
}