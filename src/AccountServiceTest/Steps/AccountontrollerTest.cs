using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using AccountService;
using System.Linq;
using System.Collections.Generic;
using AccountService.Repositories.Interfaces;
using AccountService.Controllers;
using System.Threading.Tasks;


namespace AccountServiceTest.Steps
{
  public class AccountcontrollerTest
  {
      private AccountController controller;
      private Mock<IAccountRepository> productServiceMock;
      private ILogger<AccountController> logger;

     // private Product product;
  
      //private List<CartItem> items;

    //   [@Before]
    //   public void Setup()
    //   {
          
    //       productServiceMock = new Mock<IProductRepository>();
    //       logger = new Mock<ILogger<AccountController>>().Object;
         
    //       product = new Product();
       
    //       productServiceMock.Setup(item => item.GetProducts() ).ReturnsAsync(GetProducts());

    //       controller = new AccountController(productServiceMock.Object, logger);
    //   }

    //     [Given(@"List of All products")]
    //     public void WhenTheUserGoesToTheRegisterUserScreen()
    //     {
    //         controller = new AccountController();
    //         result = controller.Register();
    //     }

    //     [When(@"products are queried by ID")]
    //     public void WhenTheUserGoesToTheRegisterUserScreen()
    //     {
    //         controller = new AccountController();
    //         result = controller.Register();
    //     }

    //     [Then(@"Corresponding product should be returned")]
    //     public void ThenTheRegisterUserViewShouldBeDisplayed()
    //     {
    //         Assert.IsInstanceOf<viewresult>(result);
    //         Assert.IsEmpty(((ViewResult)result).ViewName);
    //         Assert.AreEqual("Register", 
    //                controller.ViewData["Title"], 
    //                "Page title is wrong");
    //     }


    
  }
}