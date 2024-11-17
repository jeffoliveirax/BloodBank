using BloodBank.API.Entities;
using BloodBank.API.Models;
using BloodBank.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    [Route("api/Doadores")]
    [ApiController]
    public class DoadoresController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromServices] BloodBankDbContext db, [FromBody] CreateDoadorInputModel model)//Cadastro de doadores
        {
            var isThereAlreadyThisEmail = db.Doadores.Where(d => d.Email == model.Email).FirstOrDefault();
            if (isThereAlreadyThisEmail != null)
            {
                return BadRequest("Já existe um usuário cadastrado com este e-mail");
            }

            var doador = new Doador(model.NomeCompleto, model.Email, model.DataNascimento, model.Genero, model.Peso, model.Endereco, model.TipoSanguineo, model.FatorRh);

            db.Doadores.Add(doador);

            db.SaveChanges();

            return Ok("Usuário criado com sucesso!");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) //Consulta doador
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll() //Consulta doadores
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id) //Atualiza doador
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id) //Excluir doador
        {
            return Ok();
        }
    }
}
