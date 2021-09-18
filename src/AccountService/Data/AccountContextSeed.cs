using System;
using System.Collections.Generic;
using AccountService.Entities;
using AccountService.Settings;
using MongoDB.Driver;

namespace AccountService.Data.Interfaces
{
    public class AccountContextSeed
    {
        public static void SeedData(IMongoCollection<BankDetail> bankCollection)
        {
            bool Collection = bankCollection.Find(p => true).Any();
            if (Collection)
            {
                bankCollection.InsertManyAsync(GetPreconfiguredBanks());
            }
        }

        private static IEnumerable<BankDetail> GetPreconfiguredBanks()
        {
            return new List<BankDetail>()
            {
                new BankDetail()
                {
                    BankName = "CITI",
                    BankID = "1",
                    BankUrls =
                        new Dictionary<string, string>()
                        {
                            {
                                "AccountBalance",
                                "http://107.23.94.249/CITI/AccountDetail"
                            },
                            {
                                "AccountTransactions",
                                "http://107.23.94.249/CITI/TransactionDetail"
                            },
                            {
                                "VerifyFundPayee",
                                "http://107.23.94.249/CITI/verifypayee"
                            },
                            {
                                "GenerateOTP",
                                "http://107.23.94.249/CITI/GenerateOtp"
                            },
                            {
                                "ValidateFundTransfer",
                                "http://107.23.94.249/CITI/ValidateOtp"
                            },
                            {
                                "ProcessTransaction",
                                "http://107.23.94.249/CITI/ProcessTransaction"
                            }
                        }
                },
                new BankDetail()
                {
                    BankName = "HDFC",
                    BankID = "2",
                    BankUrls =
                        new Dictionary<string, string>()
                        {
                            {
                                "AccountBalance",
                                "http://107.23.94.249/HDFC/AccountDetail"
                            },
                            {
                                "AccountTransactions",
                                "http://107.23.94.249/HDFC/TransactionDetail"
                            },
                            {
                                "VerifyFundPayee",
                                "http://107.23.94.249/HDFC/verifypayee"
                            },
                            {
                                "GenerateOTP",
                                "http://107.23.94.249/HDFC/GenerateOtp"
                            },
                            {
                                "ValidateFundTransfer",
                                "http://107.23.94.249/HDFC/ValidateOtp"
                            },
                            {
                                "ProcessTransaction",
                                "http://107.23.94.249/HDFC/ProcessTransaction"
                            }
                        }
                },
                new BankDetail()
                {
                    BankName = "ICICI",
                    BankID = "3",
                    BankUrls =
                        new Dictionary<string, string>()
                        {
                            {
                                "AccountBalance",
                                "http://107.23.94.249/ICICI/AccountDetail"
                            },
                            {
                                "AccountTransactions",
                                "http://107.23.94.249/ICICI/TransactionDetail"
                            },
                            {
                                "VerifyFundPayee",
                                "http://107.23.94.249/ICICI/verifypayee"
                            },
                            {
                                "GenerateOTP",
                                "http://107.23.94.249/ICICI/GenerateOtp"
                            },
                            {
                                "ValidateFundTransfer",
                                "http://107.23.94.249/ICICI/ValidateOtp"
                            },
                            {
                                "ProcessTransaction",
                                "http://107.23.94.249/ICICI/ProcessTransaction"
                            }
                        }
                }
            };
        }
    }
}
