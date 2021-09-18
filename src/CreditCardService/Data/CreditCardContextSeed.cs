using System;
using System.Collections.Generic;
using CreditCardService.Settings;
using MongoDB.Driver;
using CreditCardService.Entities;

namespace CreditCardService.Data
{

    public class CreditCardContextSeed
    {
        public static void SeedData(IMongoCollection<BankDetail> bankCollection)
        {
            bool Collection = bankCollection.Find(p => true).Any();
            if(Collection)
            {
                bankCollection.InsertManyAsync(GetPreconfiguredBanks());
            }

        }

        private static IEnumerable<BankDetail> GetPreconfiguredBanks()
        {
            return new List<BankDetail>()
            {
                new BankDetail()
                {
                    BankName = "CITI",
                    BankID = "1",
                    BankUrls = new Dictionary<string,string>()
                    {
                       {"AccountBalance", "http://107.23.94.249/CITI/AccountDetail"} ,
                       {"AccountTransactions", "http://107.23.94.249/CITI/TransactionDetail"} 

                    }
                },
                new BankDetail()
                {
                   BankName = "HDFC",
                    BankID = "2",
                    BankUrls = new Dictionary<string,string>()
                    {
                       {"AccountBalance", "http://107.23.94.249/HDFC/AccountDetail"} ,
                       {"AccountTransactions", "http://107.23.94.249/HDFC/TransactionDetail"} 

                    }
                },
                new BankDetail()
                {
                    BankName = "ICICI",
                    BankID = "3",
                    BankUrls = new Dictionary<string,string>()
                    {
                       {"AccountBalance", "http://107.23.94.249/ICICI/AccountDetail"} ,
                       {"AccountTransactions", "http://107.23.94.249/ICICI/TransactionDetail"} 

                    }
                }
            };
        }
    }
}