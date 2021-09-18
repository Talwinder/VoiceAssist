using MongoDB.Driver;
using CreditCardService.Entities;

namespace CreditCardService.Data.Interfaces
{
    
    public interface ICreditCardContext
    {

        IMongoCollection<BankDetail> BankDetails { get;}




    }
}