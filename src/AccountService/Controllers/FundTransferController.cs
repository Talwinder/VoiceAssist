using AutoMapper;
using AccountService.Entities;
using AccountService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using AccountService.StaticHelpers;
using System.Security.Cryptography;

namespace AccountService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FundTransferController : ControllerBase
    {
        private readonly IFundRepository _repository;
        private readonly ILogger<FundTransferController> _logger;
      //  private readonly IMapper _mapper;
       // private readonly EventBusRabbitMQProducer _eventBus;

        public FundTransferController(IFundRepository repository, ILogger<FundTransferController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
              _logger = logger ?? throw new ArgumentNullException(nameof(logger));
          //  _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        public async Task<IActionResult>  ValidateTransaction([FromBody] FundDetail trans)
        {
             _logger.LogInformation("Request info is {FundDetail}", trans);
             try
             {
               var validatedResult = await _repository.ValidateTransaction(trans);
               return Ok(validatedResult);
             }
             catch(Exception ex)
             {
                 _logger.LogError("Error occured, {reason} ", ex.Message);
             }
            return Ok(500);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessTransaction([FromBody] FundDetail fundDetail)
        {
             _logger.LogInformation("Request info is {FundDetail}", fundDetail);
             try
             {
                   fundDetail.OTP = EncryptDecrypt.Decrypt(fundDetail.OTP);
                   fundDetail.Amount = EncryptDecrypt.Decrypt(fundDetail.Amount);
             }
              catch (CryptographicException ex)
             {
                _logger.LogError("Request info is {innerException}", ex.InnerException.ToString());
             }
           
            var trans = await _repository.ProcessTransaction(fundDetail);
            return Ok(trans);
        }



    }
}
