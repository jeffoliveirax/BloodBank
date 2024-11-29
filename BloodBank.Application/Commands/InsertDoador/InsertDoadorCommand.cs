using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using MediatR;

namespace BloodBank.Application.Commands.InsertDoador
{
    public class InsertDoadorCommand : IRequest<ResultViewModel<int>>
    {
        public InsertDoadorCommand(string nomeCompleto, string email, DateTime dataNascimento, string genero, double peso, TipoSanguineo tipoSanguineo, FatorRh fatorRh, Endereco endereco)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            Endereco = endereco;
        }

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
