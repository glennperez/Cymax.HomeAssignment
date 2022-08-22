using System;
using Company2.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly ILogger<OffersController> _logger;

        public OffersController(ILogger<OffersController> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// Post method: This method simulates Business Logic about how the company calculates its offer
        /// </summary>
        /// <param name="deal"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Offer deal)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            return Ok(new { amount = new Random().Next(7000) });
        }
    }
}

