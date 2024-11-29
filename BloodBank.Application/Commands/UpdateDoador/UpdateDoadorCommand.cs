using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using MediatR;

namespace BloodBank.Application.Commands.UpdateDoador
{
    public class UpdateDoadorCommand : IRequest<ResultViewModel>
    {
        public UpdateDoadorCommand(int id,
            string email,
            DateTime dataNascimento,
            string genero,
            double peso,
            Endereco endereco,
            TipoSanguineo
            tipoSanguineo,
            FatorRh fatorRh)
        {
            Id = id;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            Endereco = endereco;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public double Peso { get; set; }
        public Endereco Endereco { get; set; }
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
    }

}
