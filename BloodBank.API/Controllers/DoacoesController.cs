using BloodBank.Application.Commands.DeleteDoacao;
using BloodBank.Application.Commands.InsertDoacao;
using BloodBank.Application.Commands.UpdateDoacao;
using BloodBank.Application.Models;
using BloodBank.Application.Query.GetDoacaoById;
using BloodBank.Application.Query.GetDoacoesAll;
using BloodBank.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Doacoes")]
    [ApiController]
    public class DoacoesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertDoacaoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDoacaoByIdQuery(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "")
        {
            var result = await _mediator.Send(new GetDoacoesAllQuery());

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateDoacaoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>  Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDoacaoCommand(id)); 

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }


    }
}

