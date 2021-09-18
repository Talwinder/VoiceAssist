using AccountService.Data.Interfaces;
using AccountService.Entities;
using AccountService.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;


namespace AccountService.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAccountContext _context;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(IAccountContext context,  ILogger<AccountRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        //This gets bank related URLs from the stored mobdo database
        public BankDetail GetBankDetails(string bankName)
        {
             _logger.LogInformation("Request info for GetBankDetails is {bankName}", bankName);
            BankDetail bankdeatil = null;
            try
            {
                    bankdeatil =   _context
                           .BankDetails
                           .Find(p => p.BankName == bankName)
                           .FirstOrDefault();
            }
            catch (MongoException e)
            {
                 _logger.LogCritical("Error connecting to mongo db for bank details with error : {erroor}", e.Message);
            }
            return bankdeatil;

        }

        public async Task<T> GetAccountInformation<T>(string userName, string bankName, string request)
        {
             _logger.LogInformation("Request info for GetAccountInformation is {bankName},{userName},{request}", bankName,userName,request);
            var bankdeatils =  GetBankDetails(bankName);

            if (bankdeatils == null)
            {
                return default(T);
            }
            var result  =  SendReqBankAPI.SendRequest(userName,bankdeatils.BankUrls[request]);
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<AccountTransaction> GetAccountTransactions(string userName, string bankName)
        {
            var bankdeatils =  GetBankDetails(bankName);

            if (bankdeatils == null)
            {
                return null;
            }
            var result  =  SendReqBankAPI.SendRequest(userName,bankdeatils.BankUrls["AccountBalance"]);
            return JsonConvert.DeserializeObject<AccountTransaction>(result);
        }
        

        
      
        public async Task<bool> DeleteAccount(string userName)
        {
            return await _context
                            .Redis
                            .KeyDeleteAsync(userName);
        }
    }
}