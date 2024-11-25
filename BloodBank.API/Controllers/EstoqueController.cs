using BloodBank.Application.Services;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _service;
        public EstoqueController(IEstoqueService service) 
            => _service = service;

        [HttpGet]
        public IActionResult GetAll() 
        {
            var result = _service.GetAll();

            if (!result.IsSuccess) 
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("last-30-days")]
        public IActionResult GetByDate()
        {
            var result = _service.GetByDate();

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }
    }
}
