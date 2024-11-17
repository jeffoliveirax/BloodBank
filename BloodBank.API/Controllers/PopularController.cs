using BloodBank.API.Entities;
using BloodBank.API.Enum;
using BloodBank.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers
{
    //[ApiController]
    //[Route("api/popular")]
    public class PopularController : ControllerBase
    {
        [HttpGet]
        public IActionResult Popular([FromServices] BloodBankDbContext db)
        {
            //var doador1 = new Doador("Luis", "luis@luis", DateTime.Now.AddYears(-31), "M", 75.2, "Endereço 111", TipoSanguineo.O, FatorRh.Negativo);
            //var doador2 = new Doador("Henrique", "henrique@luis", DateTime.Now.AddYears(-26), "M", 85.2, "Endereço 1213", TipoSanguineo.A, FatorRh.Positivo);

            //db.Doadores.Add(doador1);
            //db.Doadores.Add(doador2);

            //db.SaveChanges();

            //var doacao1 = new Doacao(doador1.Id, DateTime.Now, 500);
            //var doacao2 = new Doacao(doador2.Id, DateTime.Now, 400);

            //db.Doacoes.Add(doacao1);
            //db.Doacoes.Add(doacao2);

            //db.SaveChanges();

            return Ok();
        }

        private void CalcularEstoque(List<Doacao> doacoes, List<Doador> doadores)
        {
            //var oNegativo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.O && d.Doador.FatorRh == FatorRh.Negativo).Sum(d => d.Volume);
            //var oPositivo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.O && d.Doador.FatorRh == FatorRh.Positivo).Sum(d => d.Volume);

            //var aNegativo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.A && d.Doador.FatorRh == FatorRh.Negativo).Sum(d => d.Volume);
            //var aPositivo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.A && d.Doador.FatorRh == FatorRh.Positivo).Sum(d => d.Volume);

            //var bNegativo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.B && d.Doador.FatorRh == FatorRh.Negativo).Sum(d => d.Volume);
            //var bPositivo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.B && d.Doador.FatorRh == FatorRh.Positivo).Sum(d => d.Volume);

            //var abNegativo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.AB && d.Doador.FatorRh == FatorRh.Negativo).Sum(d => d.Volume);
            //var abPositivo = doacoes.Where(d => d.Doador.TipoSanguineo == TipoSanguineo.AB && d.Doador.FatorRh == FatorRh.Positivo).Sum(d => d.Volume);

            var estoquesAgrupados = doacoes
                .Join(doadores,
                      doacao => doacao.DoadorId,
                      doador => doador.Id,
                      (doacao, doador) => new { doacao, doador })
                .GroupBy(d => new { d.doador.TipoSanguineo, d.doador.FatorRh })
                .Select(grupo => new
                {
                    TipoSanguineo = grupo.Key.TipoSanguineo,
                    FatorRh = grupo.Key.FatorRh,
                    Volume = grupo.Sum(d => d.doacao.Volume),
                    Quantidade = grupo.Count() // ou grupo.Sum(d => d.doacao.Quantidade) se houver uma propriedade Quantidade
                })
                .ToList();

            //return estoquesAgrupados;
        }
    }
}
