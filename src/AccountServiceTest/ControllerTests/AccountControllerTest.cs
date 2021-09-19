using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService;
using AccountService.Controllers;
using AccountService.Entities;
using AccountService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace AccountServiceTest.ControllerTest
{
    [TestFixture]
    public class AccountControllerTest
    {
        private AccountController controller;

        private Mock<IAccountRepository> accountServiceMock;

        private ILogger<AccountController> logger;

        [SetUp]
        public void Setup()
        {
            accountServiceMock = new Mock<IAccountRepository>();
            logger = new Mock<ILogger<AccountController>>().Object;

            accountServiceMock
                .Setup(item => item.GetBankDetails(""))
                .Returns(new BankDetail { BankName = "CITI" });
        }

        [Test]
        public async Task
        GetAccountBalance_ReturnsAccountBalance_WithValidValue()
        {
            var acc = new AccountDetail { Account_Balance = 50 };
            accountServiceMock
                .Setup(pa =>
                    pa
                        .GetAccountInformation<AccountDetail>("test",
                        "",
                        "AccountBalance"))
                .ReturnsAsync(acc);
            controller =
                new AccountController(accountServiceMock.Object, logger);
            var result = await controller.GetAccountBalance("test");

            Assert.IsNotNull (result);
            Assert
                .AreEqual(acc.Account_Balance,
                (
                (
                (result.Result as Microsoft.AspNetCore.Mvc.OkObjectResult).Value
                ) as
                AccountDetail
                ).Account_Balance);
        }

        [Test]
        public async Task
        GetAccountTransaction_ReturnsAccountBalance_WithValidValue()
        {
            var trans =
                new AccountTransaction {
                    Items =
                        new List<AccountTransactionItems> {
                            new AccountTransactionItems { Amount = 30 }
                        }
                };
            accountServiceMock
                .Setup(pa =>
                    pa
                        .GetAccountInformation<AccountTransaction>("test",
                        "",
                        "AccountTransaction"))
                .ReturnsAsync(trans);
            controller =
                new AccountController(accountServiceMock.Object, logger);
            var result = await controller.GetAccountTransactions("test");

            Assert.IsNotNull (result);
            Assert
                .AreEqual(trans.Items.Count,
                (
                (
                (result.Result as Microsoft.AspNetCore.Mvc.OkObjectResult).Value
                ) as
                AccountTransaction
                ).Items.Count);
        }
    }
}
