namespace BloodBank.Core.Entities
{
    public class Doacao : BaseEntity
    {
        protected Doacao() { }
        public Doacao(int doadorId, decimal volume) : base()
        {
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

        public int DoadorId { get; set; }
        public DateTime Data { get; set; }
        public decimal Volume { get; set; }
        public Doador Doador { get; set; }

        public void Update(int doadorId, decimal volume)
        {
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

    }

}
