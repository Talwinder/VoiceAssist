using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Entities;

namespace AccountService.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<T>
        GetAccountInformation<T>(
            string userName, string bankName, string request
        );

        Task<AccountTransaction>
        GetAccountTransactions(
            string userName, string bankName
        );

        BankDetail GetBankDetails(string bankName);

        Task<bool> DeleteAccount(string userName);
    }
}
