using System.Collections.Generic;

namespace AccountService.Entities
{
    public class AccountDetail
    {
        public string Account_Holder { get; set; }

        public string Account_Number { get; set; }

        public string Account_Type { get; set; }

        public int Account_Balance { get; set; }

        public string UserName { get; set; }

        public string BankID { get; set; }
    }
}
