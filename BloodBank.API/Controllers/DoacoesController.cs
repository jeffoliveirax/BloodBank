using BloodBank.Application.Models;
using BloodBank.Application.Services;
using BloodBank.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Doacoes")]
    [ApiController]
    public class DoacoesController : ControllerBase
    {
        private readonly IDoacaoService _service;
        public DoacoesController(IDoacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post(CreateDoacaoInputModel model)
        {
            var result = _service.Insert(model);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        public IActionResult GetAll(string search = "")
        {
            var result = _service.GetAll();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateDoacaoInputModel model)
        {
            var result = _service.Update(id, model);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id); 

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }


    }
}

