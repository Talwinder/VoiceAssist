using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Entities
{
    public class AccountTransaction
    {

        public int AccountID { get; set; }
        public List<AccountTransactionItems> Items { get; set; } = new List<AccountTransactionItems>();
        public string BankID {get; set;}
    }
}