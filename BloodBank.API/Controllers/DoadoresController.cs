using Microsoft.AspNetCore.Mvc;
using MediatR;
using BloodBank.Application.Commands.InsertDoador;
using BloodBank.Application.Query.GetDoadorById;
using BloodBank.Application.Query.GetDoadoresAll;
using BloodBank.Application.Commands.UpdateDoador;
using BloodBank.Application.Commands.DeleteDoador;

namespace BloodBank.API.Controllers
{
    [Route("api/Doadores")]
    [ApiController]
    public class DoadoresController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoadoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertDoadorCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDoadorByIdQuery(id));

            if (!result.IsSuccess) 
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetDoadoresAllQuery(pageNumber, pageSize));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateDoadorCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDoadorCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
