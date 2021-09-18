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

        private Mock<IFundRepository> accountServiceMock;

        private ILogger<FundTransferController> logger;
    }
}
