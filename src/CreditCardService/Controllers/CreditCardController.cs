using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CreditCardService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CreditCardService.Entities;

namespace CreditCardService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _repository;
        private readonly ILogger<CreditCardController> _logger;


        public CreditCardController(ICreditCardRepository repository, ILogger<CreditCardController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreditCardDetail>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CreditCardDetail>>> GetCreditCardDetail()
        {
            string userName = ""; string bankName= "CITI";
            var products = await _repository.GetCreditCardDetail(userName,bankName);

            return Ok(products);
        }
        
        // [HttpGet("{id:length(24)}", Name = "GetProduct")]
        // [ProducesResponseType((int)HttpStatusCode.NotFound)]
        // [ProducesResponseType(typeof(CreditCardDetail), (int)HttpStatusCode.OK)]
        // public async Task<ActionResult<CreditCardDetail>> GetProductById(string id)
        // {
        //     var product = await _repository.GetProduct(id);

        //     if (product == null)
        //     {
        //         _logger.LogError($"Product with id: {id}, not found.");
        //         return NotFound();
        //     }

        //     return Ok(product);
        // }

        // [Route("[action]/{category}")]
        // [HttpGet]
        // [ProducesResponseType(typeof(IEnumerable<CreditCardDetail>), (int)HttpStatusCode.OK)]
        // public async Task<ActionResult<IEnumerable<CreditCardDetail>>> GetProductByCategory(string category)
        // {
        //     var products = await _repository.GetProductByCategory(category);
        //     return Ok(products);
        // }

        // [HttpPost]
        // [ProducesResponseType(typeof(CreditCardDetail), (int)HttpStatusCode.OK)]
        // public async Task<ActionResult<CreditCardDetail>> CreateProduct([FromBody] CreditCardDetail product)
        // {
        //     await _repository.Create(product);

        //     return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        // }

        // [HttpPut]
        // [ProducesResponseType(typeof(CreditCardDetail), (int)HttpStatusCode.OK)]
        // public async Task<IActionResult> UpdateProduct([FromBody] CreditCardDetail product)
        // {
        //     return Ok(await _repository.Update(product));
        // }

        // [HttpDelete("{id:length(24)}")]
        // [ProducesResponseType(typeof(CreditCardDetail), (int)HttpStatusCode.OK)]
        // public async Task<IActionResult> DeleteProductById(string id)
        // {
        //     return Ok(await _repository.Delete(id));
        // }
    }
}