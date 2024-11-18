using BloodBank.Core.Entities;
using BloodBank.Core.Enum;

namespace BloodBank.Application.Models
{
    public class UpdateDoadorInputModel
    {
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public double Peso { get; set; }
        public Endereco Endereco { get; set; }
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
    }

}
