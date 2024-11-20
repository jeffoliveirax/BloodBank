using BloodBank.Core.Enum;

namespace BloodBank.Core.Entities
{
    public class Estoque : BaseEntity
    {
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
        public decimal Volume { get; set; }
    }
}
