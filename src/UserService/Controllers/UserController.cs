
using UserService.Entities;
using UserService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;

using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

namespace UserService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
      //  private readonly IMapper _mapper;
       // private readonly EventBusRabbitMQProducer _eventBus;

        public UserController(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
          //  _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDetail), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDetail>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new UserDetail());
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ValidateUserPin([FromBody] UserDetail user)
        {

            return Ok(await _repository.ValidateUserPin(user));
        }

        
        public string GetUserName(string code)
        {
          string idToken =  HttpContext.GetTokenAsync("id_token").Result;
             GetUserRequest getUserRequest = new GetUserRequest();
                getUserRequest.AccessToken = idToken;

                var getTheUser = new AmazonCognitoIdentityProviderClient("","","").GetUserAsync(getUserRequest);

                var userAttributes = getTheUser.Result.UserAttributes;

                return userAttributes.ToString();
         }
         

      

    }
}
