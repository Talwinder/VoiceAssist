using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardService.Entities
{
    public class CreditCardTransaction
    {

        public int AccountID { get; set; }
        public List<CreditTransactionItems> Items { get; set; } = new List<CreditTransactionItems>();
        public string BankID {get; set;}
    }
}