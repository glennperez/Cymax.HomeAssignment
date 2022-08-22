using System;
using Company1.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly ILogger<DealsController> _logger;

        public DealsController(ILogger<DealsController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Post method: This method simulates Business Logic about how the company calculates its offer.
        /// </summary>
        /// <param name="deal"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Deal deal)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();
            
            return Ok(new { total = new Random().Next(7000) });
        }
    }
}

