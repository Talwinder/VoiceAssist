using AccountService.Entities;
using MongoDB.Driver;
using StackExchange.Redis;

namespace AccountService.Data.Interfaces
{
    public interface IAccountContext
    {
        IDatabase Redis { get; }

        BankDetail GetBankDetails(string bankName);
    }
}
