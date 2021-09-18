using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Entities
{
    public class AccountTransactionItems
    {
        public DateTime DateOftransaction { get; set; }
        public int TransactionTypeID { get; set; }
        public double Amount { get; set; }
       
    }
}