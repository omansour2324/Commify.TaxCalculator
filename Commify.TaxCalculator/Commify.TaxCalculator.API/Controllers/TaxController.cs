using Commify.TaxCalculator.API.Logic;
using Commify.TaxCalculator.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commify.TaxCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxCalculatorService _service;

        public TaxController(ITaxCalculatorService service)
        {
            _service = service;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<TaxCalculationResult>> Calculate([FromBody] SalaryInput input)
        {
            if (input.GrossSalary <= 0)
                return BadRequest("Gross salary must be greater than zero.");

            var result = await _service.CalculateTaxAsync(input.GrossSalary);
            return Ok(result);
        }
    }
}
