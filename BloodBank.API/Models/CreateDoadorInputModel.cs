using BloodBank.API.Entities;
using BloodBank.API.Enum;

namespace BloodBank.API.Models
{
    public class CreateDoadorInputModel
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public double Peso { get; set; }
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
        public Endereco Endereco { get; set; }
    }
}
