using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Entities;

namespace AccountService.Repositories.Interfaces
{
    public interface IFundRepository
    {
        Task<string> ValidateTransaction(FundDetail fundDetail);

        Task<string> ProcessTransaction(FundDetail fundDetail);
    }
}
