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

        [HttpPost]
        public IActionResult Post([FromBody] Offer deal)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            return Ok(new { amount = new Random().Next(7000) });
        }
    }
}

