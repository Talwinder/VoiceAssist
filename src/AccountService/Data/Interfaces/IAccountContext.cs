
 using AccountService.Entities;
using StackExchange.Redis;
using MongoDB.Driver;
namespace AccountService.Data.Interfaces

{

    public interface IAccountContext
    {
        IDatabase Redis { get; }
        IMongoCollection<BankDetail> BankDetails { get; set;}
    }

}