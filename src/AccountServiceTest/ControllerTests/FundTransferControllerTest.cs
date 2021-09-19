using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService;
using AccountService.Controllers;
using AccountService.Entities;
using AccountService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AccountServiceTest.ControllerTest
{
    [TestFixture]
    public class FundTransferControllerTest
    {
        private FundTransferController controller;

        private Mock<IFundRepository> fundServiceMock;

        private ILogger<FundTransferController> logger;

        [SetUp]
        public void Setup()
        {
            fundServiceMock = new Mock<IFundRepository>();
            logger = new Mock<ILogger<FundTransferController>>().Object;

            // fundServiceMock
            //     .Setup(item => item.GetBankDetails(""))
            //     .Returns(new BankDetail { BankName = "CITI" });
        }

        [Test]
        public async Task
        ValidateTransaction_ReturnsOKRespose_WithOTPSentMessage()
        {
            var trans =
                new FundDetail { Payee = "talwinders", BankName = "CITI" };
            fundServiceMock
                .Setup(pa => pa.ValidateTransaction(trans))
                .ReturnsAsync("OTP sent successfully");
            controller =
                new FundTransferController(fundServiceMock.Object, logger);
            var result = await controller.ValidateTransaction(trans);

            Assert.IsNotNull (result);
            Assert
                .AreEqual("OTP sent successfully",
                (result as Microsoft.AspNetCore.Mvc.OkObjectResult).Value);
        }
    }
}
