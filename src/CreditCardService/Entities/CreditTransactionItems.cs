using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardService.Entities
{
    public class CreditTransactionItems
    {
        public DateTime DateOftransaction { get; set; }
        public int TransactionTypeID { get; set; }
        public double Amount { get; set; }
       
    }
}