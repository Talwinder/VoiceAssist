
using UserService.Entities;
using UserService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using MassTransit;
using EventBusMQ.Events;

namespace UserService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
       // private readonly EventBusRabbitMQProducer _eventBus;

        public UserController(IUserRepository repository,IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            //_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

      

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ValidateUserPin([FromBody] UserDetail user)
        {
            var result = await _repository.ValidateUserPin(user);
            if(result == "Valid Pin")
            {
                var eventMessage = _mapper.Map<UpdateUserTokenEvent>(user.UserToken);
                await _publishEndpoint.Publish(eventMessage);
            }
            return Ok(result);
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
