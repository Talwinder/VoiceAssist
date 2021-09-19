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
using AccountService.Repositories.Interfaces.StaticClassesInterfaces;


namespace AccountService.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAccountContext _context;
        private readonly ILogger<AccountRepository> _logger;
        private readonly IJsonUtility _jsonUtility;
        private readonly ISendReqBankAPI _sendReqBankAPI;

        public AccountRepository(IAccountContext context,  ILogger<AccountRepository> logger, IJsonUtility jsonUtility, ISendReqBankAPI sendReqBankAPI)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
            _jsonUtility = jsonUtility;
            _sendReqBankAPI= sendReqBankAPI;
        }

        //This gets bank related URLs from the stored mobdo database
        public  BankDetail GetBankDetails(string bankName)
        {
             _logger.LogInformation("Request info for GetBankDetails is {bankName}", bankName);
            BankDetail bankDetail = null;
            try
            {
                    bankDetail =   _context
                           .GetBankDetails(bankName);       
            }
            catch (MongoException e)
            {
                 _logger.LogCritical("Error connecting to mongo db for bank details with error : {erroor}", e.Message);
            }
            return bankDetail;

        }

        public async Task<T> GetAccountInformation<T>(string userName, string bankName, string request)
        {
             _logger.LogInformation("Request info for GetAccountInformation is {bankName},{userName},{request}", bankName,userName,request);
            var bankdeatils =  GetBankDetails(bankName);

            if (bankdeatils == null)
            {
                return default(T);
            }
            var result  =  _sendReqBankAPI.SendRequest(userName,bankdeatils.BankUrls[request]);
            return _jsonUtility.DeserializeObject<T>(result);
        }

        public async Task<AccountTransaction> GetAccountTransactions(string userName, string bankName)
        {
            var bankdeatils =  GetBankDetails(bankName);

            if (bankdeatils == null)
            {
                return null;
            }
            var result  =  _sendReqBankAPI.SendRequest(userName,bankdeatils.BankUrls["AccountTransactions"]);
            return _jsonUtility.DeserializeObject<AccountTransaction>(result);
        }
        

        
      
     
    }
}