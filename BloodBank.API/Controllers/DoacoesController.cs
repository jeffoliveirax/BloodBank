using BloodBank.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Doacoes")]
    [ApiController]
    public class DoacoesController : ControllerBase
    {
        // POST api/Doacoes
        [HttpPost]
        public IActionResult Post(CreateDoacaoInputModel model)//Registro de doacoes
        {
            return CreatedAtAction(nameof(GetById), new { id = 1}, model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateDoacaoInputModel model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }


    }
}
