using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BloodBank.Application.Models
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

        public Doador ToEntity()
            => new(NomeCompleto, Email, DataNascimento, Genero, Peso, Endereco, TipoSanguineo, FatorRh);
    }
}
