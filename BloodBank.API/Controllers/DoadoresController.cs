using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using BloodBank.Application.Services;

namespace BloodBank.API.Controllers
{
    [Route("api/Doadores")]
    [ApiController]
    public class DoadoresController : ControllerBase
    {
        private readonly IDoadorService _service;
        public DoadoresController(IDoadorService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post(CreateDoadorInputModel model)
        {
            var result = _service.Insert(model);

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
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = _service.GetAll(pageNumber, pageSize);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateDoadorInputModel model)
        {
            var result = _service.Update(id, model);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (_service.Delete(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
