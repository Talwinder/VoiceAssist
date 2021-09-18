using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

using NUnit.Framework;

using Moq;

using AccountService.Controllers;

namespace AccountServiceTest.Steps
{
    [Binding]
    public class RegisterUserSteps
    {
        // ActionResult result;
        // AccountController controller;

        // [When(@"the user goes to the register user screen")]
        // public void WhenTheUserGoesToTheRegisterUserScreen()
        // {
        //     controller = new AccountController();
        //     result = controller.Register();
        // }

        // [Then(@"the register user view should be displayed")]
        // public void ThenTheRegisterUserViewShouldBeDisplayed()
        // {
        //     Assert.IsInstanceOf<viewresult>(result);
        //     Assert.IsEmpty(((ViewResult)result).ViewName);
        //     Assert.AreEqual("Register", 
        //            controller.ViewData["Title"], 
        //            "Page title is wrong");
        // }
    }
}