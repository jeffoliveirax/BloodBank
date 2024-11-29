using BloodBank.Core.Enum;

namespace BloodBank.Core.Entities
{
    public class Estoque : BaseEntity
    {
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
        public decimal Volume { get; set; }

        public static List<Estoque> CalcularEstoque(List<Doacao> doacoes, List<Doador> doadores)
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
