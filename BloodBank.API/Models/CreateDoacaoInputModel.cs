using BloodBank.API.Entities;

namespace BloodBank.API.Models
{
    public class CreateDoacaoInputModel
    {
        public CreateDoacaoInputModel(int doadorId, decimal volume)
        {
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; set; }

        public Doacao ToEntity()
            => new(DoadorId, Volume);
    }
}
