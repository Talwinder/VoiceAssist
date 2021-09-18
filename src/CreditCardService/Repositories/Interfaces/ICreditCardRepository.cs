using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using CreditCardService.Entities;

namespace CreditCardService.Repositories.Interfaces
{

    public interface ICreditCardRepository
    {

       // Task<IEnumerable<Product>> GetProducts();

        Task<CreditCardDetail> GetCreditCardDetail(string userName, string bankName);

      //  Task<IEnumerable<Product>> GetProductByName(string name);

      //  Task<IEnumerable<Product>> GetProductByCategory(string Category);

 



    }
}