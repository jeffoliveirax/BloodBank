using BloodBank.Core.Entities;
using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Doadores")]
    [ApiController]
    public class DoadoresController : ControllerBase
    {
        private readonly BloodBankDbContext _db;
        public DoadoresController(BloodBankDbContext db) => _db = db;

        [HttpPost]
        public IActionResult Post([FromServices] BloodBankDbContext db, [FromBody] CreateDoadorInputModel model)
        {
            var isThereAlreadyThisEmail = db.Doadores.Where(d => d.Email == model.Email).FirstOrDefault();
            if (isThereAlreadyThisEmail != null)
            {
                return BadRequest("Já existe um usuário cadastrado com este e-mail");
            }

            var doador = new Doador(
                model.NomeCompleto,
                model.Email,
                model.DataNascimento,
                model.Genero,
                model.Peso,
                model.Endereco,
                model.TipoSanguineo,
                model.FatorRh);

            db.Doadores.Add(doador);

            db.SaveChanges();

            return Ok("Usuário criado com sucesso!");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var doador = _db.Doadores.Where(d => d.Id == id).FirstOrDefault();

            if (doador is null)
                return NotFound();

            return Ok(doador);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var totalItems = _db.Doadores.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var doadores = _db.Doadores
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = doadores
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateDoadorInputModel model)
        {
            var doador = _db.Doadores.SingleOrDefault(d => d.Id == id);

            if (doador is null) return NotFound();

            doador.Update(
                model.Email,
                model.DataNascimento,
                model.Genero,
                model.Peso,
                model.Endereco,
                model.TipoSanguineo,
                model.FatorRh);

            _db.Doadores.Update(doador);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var doador = _db.Doadores.SingleOrDefault(d => d.Id == id);

            if (doador is null)
                return NotFound();

            doador.SetAsDeleted();

            _db.Doadores.Update(doador);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
