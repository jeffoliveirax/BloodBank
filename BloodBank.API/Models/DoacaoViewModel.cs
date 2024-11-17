using BloodBank.API.Entities;

namespace BloodBank.API.Models
{
    public class DoacaoViewModel
    {
        public DoacaoViewModel(int id, int doadorId, DateTime data, decimal volume, string nomeDoador)
        {
            Id = id;
            DoadorId = doadorId;
            Data = data;
            Volume = volume;
            NomeDoador = nomeDoador;
        }

        public int Id { get; private set; }
        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; private set; }
        public string NomeDoador { get; private set; }

        public static DoacaoViewModel FromEntity(Doacao doacao)
            => new(doacao.Id, doacao.DoadorId, doacao.Data, doacao.Volume, doacao.Doador.NomeCompleto);
    }

}
