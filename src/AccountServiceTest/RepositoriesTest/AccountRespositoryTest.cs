using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccountService;
using AccountService.Controllers;
using AccountService.Data.Interfaces;
using AccountService.Entities;
using AccountService.Repositories;
using AccountService.Repositories.Interfaces;
using AccountService.Repositories.Interfaces.StaticClassesInterfaces;
using AccountService.Repositories.Wrappers;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccountServiceTest.RepositoriesTest
{
    [TestFixture]
    public class AccountRespositoryTest
    {
        private AccountRepository repository;

        private ILogger<AccountRepository> logger;

        private Mock<IJsonUtility> mockJsonUtility;

        private Mock<IAccountContext> contextMock;

        private Mock<ISendReqBankAPI> sendReqBankAPIMock;

        [SetUp]
        public void Setup()
        {
            contextMock = new Mock<IAccountContext>();
            sendReqBankAPIMock = new Mock<ISendReqBankAPI>();
            logger = new Mock<ILogger<AccountRepository>>().Object;

            contextMock
                .Setup(item => item.GetBankDetails("testBank"))
                .Returns(GetPreconfiguredBank());

            mockJsonUtility = new Mock<IJsonUtility>();
        }

        [Test]
        public async Task
        GetAccountInformation_ReturnsAccountBalance_WithValidValue()
        {
            var acc = new AccountDetail { Account_Balance = 30 };
            JObject accj =
                JObject
                    .Parse(File.ReadAllText(@".\TestJsons\AccountDetail.json"));

            mockJsonUtility
                .Setup(t =>
                    t.DeserializeObject<AccountDetail>(It.IsAny<string>()))
                .Returns(acc);
            sendReqBankAPIMock
                .Setup(t => t.SendRequest("talwinders", "testBankURL"))
                .Returns(accj.ToString());
            Console.Write(accj.ToString());
            repository =
                new AccountRepository(contextMock.Object,
                    logger,
                    mockJsonUtility.Object,
                    sendReqBankAPIMock.Object);

            var result =
                await repository
                    .GetAccountInformation<AccountDetail>("talwinders",
                    "testBank",
                    "testBankURL");

            Assert.IsNotNull (result);
            Assert.AreEqual(acc.Account_Balance, result.Account_Balance);
        }

        private BankDetail GetPreconfiguredBank()
        {
            return new BankDetail()
            {
                BankName = "testBank",
                BankID = "1",
                BankUrls =
                    new Dictionary<string, string>()
                    {
                        { "testBankURL", "testBankURL" },
                        {
                            "AccountTransactions",
                            "http://107.23.94.249/CITI/TransactionDetail"
                        }
                    }
            };
        }
    }
}
