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
            var doador = _db.Doadores.SingleOrDefault(d => d.Id == model.DoadorId);
            if (doador == null)
            {
                return NotFound("Id de doador não existe!");
            }

            var idade = DateTime.Today.Year - doador.DataNascimento.Year;
            if (doador.DataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < 18)
            {
                return BadRequest("O doador deve ter mais de 18 anos para efetuar uma doação.");
            }

            if (doador.Peso < 50)
            {
                return BadRequest("O doador deve pesar no mínimo 50kg para efetuar uma doação.");
            }

            if (doador.Genero.ToLower() == "feminino")
            {
                var ultimaDoacao = _db.Doacoes
                    .Where(d => d.DoadorId == doador.Id)
                    .OrderByDescending(d => d.Data)
                    .FirstOrDefault();

                if (ultimaDoacao != null && (DateTime.Today - ultimaDoacao.Data).TotalDays < 90)
                {
                    return BadRequest("Mulheres só podem doar de 90 em 90 dias.");
                }
            }

            if (doador.Genero.ToLower() == "masculino")
            {
                var ultimaDoacao = _db.Doacoes
                    .Where(d => d.DoadorId == doador.Id)
                    .OrderByDescending(d => d.Data)
                    .FirstOrDefault();

                if (ultimaDoacao != null && (DateTime.Today - ultimaDoacao.Data).TotalDays < 60)
                {
                    return BadRequest("Homens só podem doar de 60 em 60 dias.");
                }
            }

            if (model.Volume < 420 || model.Volume > 470)
            {
                return BadRequest("A quantidade de mililitros de sangue doados deve ser entre 420ml e 470ml.");
            }

            var doacao = model.ToEntity();
            _db.Doacoes.Add(doacao);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = doacao.Id }, model);
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

