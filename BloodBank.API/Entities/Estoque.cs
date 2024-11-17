using BloodBank.API.Enum;

namespace BloodBank.API.Entities
{
    public class Estoque : BaseEntity
    {
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
        public decimal Volume { get; set; }
    }
}
