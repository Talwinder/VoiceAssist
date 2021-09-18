using MongoDB.Driver;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CreditCardService.Data.Interfaces;
using System;
using CreditCardService.Repositories.Interfaces;
using CreditCardService.Entities;

namespace CreditCardService.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly ICreditCardContext _context;

        public CreditCardRepository(ICreditCardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // public async Task<IEnumerable<Product>> GetProducts()
        // {
        //     return await _context
        //                     .Products
        //                     .Find(p => true)
        //                     .ToListAsync();
        // }

        public async Task<CreditCardDetail> GetCreditCardDetail(string userName, string bankName)
        {
            var  bankdeatils  = await _context
                           .BankDetails
                           .Find(p => p.BankName == bankName)
                           .FirstOrDefaultAsync();
            var result  =  SendReqBankAPI.SendRequest(userName,bankdeatils.BankUrls["CreditCardDetail"]);
            return JsonConvert.DeserializeObject<CreditCardDetail>(result);
        }

        // public async Task<IEnumerable<Product>> GetProductByName(string name)
        // {
        //     FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

        //     return await _context
        //                     .Products
        //                     .Find(filter)
        //                     .ToListAsync();
        // }

        // public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        // {
        //     FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Category, categoryName);

        //     return await _context
        //                     .Products
        //                     .Find(filter)
        //                     .ToListAsync();
        // }


        // public async Task Create(Product product)
        // {
        //     await _context.Products.InsertOneAsync(product);
        // }

        // public async Task<bool> Update(Product product)
        // {
        //     var updateResult = await _context
        //                                 .Products
        //                                 .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

        //     return updateResult.IsAcknowledged
        //             && updateResult.ModifiedCount > 0;
        // }

        // public async Task<bool> Delete(string id)
        // {
        //     FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        //     DeleteResult deleteResult = await _context
        //                                         .Products
        //                                         .DeleteOneAsync(filter);

        //     return deleteResult.IsAcknowledged
        //         && deleteResult.DeletedCount > 0;
        // }

      
    }
}
