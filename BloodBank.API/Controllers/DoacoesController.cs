using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.API.Controllers
{
    [Route("api/Doacoes")]
    [ApiController]
    public class DoacoesController : ControllerBase
    {
        private readonly BloodBankDbContext _db;
        public DoacoesController(BloodBankDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Post(CreateDoacaoInputModel model)
        {
            var doacao = model.ToEntity();

            _db.Doacoes.Add(doacao);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var doacoes = _db.Doacoes
                .Include(d => d.Doador)
                .SingleOrDefault(d => d.Id == id);

            if (doacoes is null)
                return NotFound();

            var model = DoacaoViewModel.FromEntity(doacoes);

            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var doacoes = _db.Doacoes
                .Include(d => d.Doador)
                .ToList();

            var model = doacoes.Select(DoacoesItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateDoacaoInputModel model)
        {
            var doacao = _db.Doacoes.SingleOrDefault(d => d.Id == id);

            if (doacao is null)
                return NotFound();

            doacao.Update(model.DoadorId, model.Volume);

            _db.Doacoes.Update(doacao);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var doacao = _db.Doacoes.SingleOrDefault(d => d.Id == id);

            if (doacao is null)
                return NotFound();

            doacao.SetAsDeleted();

            _db.Doacoes.Update(doacao);
            _db.SaveChanges();

            return NoContent();
        }


    }
}

