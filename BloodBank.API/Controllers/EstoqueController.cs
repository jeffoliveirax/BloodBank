using BloodBank.API.Entities;
using BloodBank.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.API.Controllers
{
    [Route("api/Estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        //GET api/Estoque
        [HttpGet]
        public IActionResult GetAll([FromServices] BloodBankDbContext db) //consulta total sangue por tipo disponível
        {
            List<Estoque> estoque = CalcularEstoque(db.Doacoes.ToList(), db.Doadores.ToList());

            if (estoque.Any())
            {
                return Ok(estoque);
            }
            else
            {
                return Ok("Não há estoque disponível!");
            }
        }

        //consulta doações últimos 30 dias e retorna dados dos doadores
        [HttpGet("last-30-days")]
        public IActionResult GetByDate([FromServices] BloodBankDbContext db)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);

            var doacoes = db.Doacoes
                            .Where(d => d.Data >= startDate && d.Data <= endDate)
                            .ToList();

            if (doacoes.Any())
            {
                List<Estoque> estoque = CalcularEstoque(doacoes, db.Doadores.ToList());

                return Ok(estoque);
            }
            else
            {
                return Ok("Não há doações no período!");
            }
        }

        private List<Estoque> CalcularEstoque(List<Doacao> doacoes, List<Doador> doadores)
        {
            var estoque = doacoes
                .Join(doadores,
                      doacao => doacao.DoadorId,
                      doador => doador.Id,
                      (doacao, doador) => new { doacao, doador })
                .GroupBy(d => new { d.doador.TipoSanguineo, d.doador.FatorRh })
                .Select(grupo => new Estoque
                {
                    TipoSanguineo = grupo.Key.TipoSanguineo,
                    FatorRh = grupo.Key.FatorRh,
                    Volume = grupo.Sum(d => d.doacao.Volume)
                })
                .ToList();

            return estoque;
        }


    }
}
