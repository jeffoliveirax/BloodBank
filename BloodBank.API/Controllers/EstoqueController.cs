using BloodBank.Application.Query.GetEstoque;
using BloodBank.Application.Query.GetEstoqueLast30Days;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EstoqueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await _mediator.Send(new GetEstoqueAllQuery());

            if (!result.IsSuccess) 
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("last-30-days")]
        public async Task<IActionResult> GetByDate()
        {
            var result = await _mediator.Send(new GetEstoqueLast30DaysQuery());

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }
    }
}
