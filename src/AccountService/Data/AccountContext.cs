using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Data.Interfaces;
using AccountService.Entities;
using AccountService.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StackExchange.Redis;

namespace AccountService.Data
{
    public class AccountContext : IAccountContext
    {
        private readonly MongoClient _mongoClient;

        private readonly IMongoCollection<BankDetail> _bankDetails;

        ILogger<AccountContext> _logger;

        public AccountContext(
            ConnectionMultiplexer redisConnection,
            IAccountDatabaseSettings settings,
            ILogger<AccountContext> logger
        )
        {
            try
            {
                Redis = redisConnection.GetDatabase();
            }
            catch (RedisConnectionException)
            {
                // redisConnection.Reconnect();
                _logger
                    .LogWarning("Redis Connection Failed . Trying reconnect");
                Redis = redisConnection.GetDatabase();
            }

            _mongoClient = new MongoClient(settings.ConnectionString);
            var database = _mongoClient.GetDatabase(settings.DatabaseName);
            _bankDetails =
                database.GetCollection<BankDetail>(settings.CollectionName);
            AccountContextSeed.SeedData (_bankDetails);
        }

        public IDatabase Redis { get; }

        public BankDetail GetBankDetails(string bankName)
        {
            return _bankDetails
                .Find(p => p.BankName == bankName)
                .FirstOrDefault();
        }
    }
}
