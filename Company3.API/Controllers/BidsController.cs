using System;
using System.Collections.Generic;
using Company3.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly ILogger<BidsController> _logger;

        public BidsController(ILogger<BidsController> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// Post method: This method simulates Business Logic about how the company calculates its offer.
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult Post([FromBody]Bid bid)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            return Ok(new Response());
        }
    }
}

