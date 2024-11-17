using BloodBank.API.Controllers;
using BloodBank.API.Enum;

namespace BloodBank.API.Entities
{
    public class Doador : BaseEntity
    {
        protected Doador() { }
        public Doador(string nomeCompleto, string email, DateTime dataNascimento, string genero, double peso, Endereco endereco, TipoSanguineo tipoSanguineo, FatorRh fatorRh) : base()
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            Endereco = endereco;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            Doacoes = new List<Doacao>();
        }

        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public double Peso { get; set; }
        public Endereco Endereco { get; set; }
        public TipoSanguineo TipoSanguineo { get; set; }
        public FatorRh FatorRh { get; set; }
        public List<Doacao> Doacoes { get; set; }

        public void Create()
        {

        }

        public void Update()
        {

        }


    }
}
