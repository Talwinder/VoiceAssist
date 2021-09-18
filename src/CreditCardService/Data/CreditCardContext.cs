using CreditCardService.Settings;
using MongoDB.Driver;
using CreditCardService.Entities;
using CreditCardService.Data.Interfaces;

namespace CreditCardService.Data
{

    public class CreditCardContext : ICreditCardContext
    {
        public CreditCardContext(ICreditCardDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database  = client.GetDatabase(settings.DatabaseName);
            BankDetails = database.GetCollection<BankDetail>(settings.CollectionName);
            CreditCardContextSeed.SeedData(BankDetails);
        }

        public IMongoCollection<BankDetail> BankDetails  { get;}
    }
}