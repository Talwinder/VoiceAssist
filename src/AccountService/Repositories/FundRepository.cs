using AccountService.Data.Interfaces;
using AccountService.Entities;
using AccountService.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace AccountService.Repositories
{
    public class FundRepository : IFundRepository
    {
        private readonly IAccountContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FundRepository> _logger;

        public FundRepository(IAccountContext context,IConfiguration configuration,ILogger<FundRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private BankDetail GetBankDetails(string bankName)
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

        public async Task<string> ValidateTransaction(FundDetail fundDetail)
        {
            Dictionary<string,string> args = new Dictionary<string,string>();
            var response  =  SendRequest(fundDetail,args,"ValidatePayee");
            if (response is null || !response.IsSuccessStatusCode)
            {
                _logger.LogWarning( "Payee not found {fundDetail}", fundDetail);
            }
            if (response.IsSuccessStatusCode)
            {
                var customerJsonString = await response.Content.ReadAsStringAsync();
                return await GenerateOtp(fundDetail);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return  await response.Content.ReadAsStringAsync();
            }
        
        }

        public async Task<string> ProcessTransaction(FundDetail fundDetail)
        {
            Dictionary<string,string> args = new Dictionary<string,string>();
            args.Add("OTP", fundDetail.OTP);
            var result  =  SendRequest(fundDetail,args,"ProcessTransaction");
            return await result.Content.ReadAsStringAsync();
        }

         public async Task<string> GenerateOtp(FundDetail fundDetail)
        {
            var result  =  SendRequest(fundDetail,null,"GenerateOTP");
            return await result.Content.ReadAsStringAsync();
        } 

        HttpResponseMessage  SendRequest(FundDetail fundDetail,Dictionary<string,string> args, string api )
        {
            string accessToken  = _configuration.GetSection("BankApiToken")["AccessToken"].ToString();
            string[] scopes = new string[]{"openApi"};
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var bankdeatils =  GetBankDetails(fundDetail.BankName);
            client.DefaultRequestHeaders.Add("userName", fundDetail.UserName);
            client.DefaultRequestHeaders.Add("bankName", bankdeatils.BankUrls[api]);
            client.DefaultRequestHeaders.Add("Payee", fundDetail.Payee);
            client.DefaultRequestHeaders.Add("Amount", fundDetail.Amount);
            if(args != null)
            foreach (var item in args)
            {
                 client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
            return  client.GetAsync(bankdeatils.BankUrls[api]).Result;
        }
        

        
      
        public async Task<bool> DeleteAccount(string userName)
        {
            return await _context
                            .Redis
                            .KeyDeleteAsync(userName);
        }
    }
}