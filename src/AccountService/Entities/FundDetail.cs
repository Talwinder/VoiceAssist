using System;
using System.Collections.Generic;

namespace AccountService.Entities
{
    public class FundDetail
    {
        public string Payee { get; set; }

        public string BankName { get; set; }

        public string Amount { get; set; }

        public string OTP { get; set; }

        public DateTime Schedule { get; set; }

        public string UserName { get; set; }
    }
}
