using AutoMapper;
using AccountService.Entities;
using AccountService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AccountService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly ILogger<AccountController> _logger;
      //  private readonly IMapper _mapper;
       // private readonly EventBusRabbitMQProducer _eventBus;

        public AccountController(IAccountRepository repository,ILogger<AccountController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
          //  _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountDetail), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountDetail>> GetAccountBalance(string userName)
        {
            _logger.LogInformation("Request info is {@userName}", userName);
            var Account = await _repository.GetAccountInformation<AccountDetail>(userName,"","AccountBalance");
            return Ok(Account ?? new AccountDetail());
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountTransaction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountTransaction>> GetAccountTransactions(string userName)
        {

             _logger.LogInformation("Request info is {@userName}", userName);
            var Account = await _repository.GetAccountInformation<AccountTransaction>(userName,"","AccountTransaction");
            return Ok(Account ?? new AccountTransaction());
        }

    }
}
