namespace BloodBank.API.Entities
{
    public class Doacao : BaseEntity
    {
        protected Doacao() { }
        public Doacao(int doadorId, DateTime data, decimal volume) : base()
        {
            DoadorId = doadorId;
            Data = data;
            Volume = volume;
        }

        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; private set; }
        public Doador Doador { get; private set; }

        public void Create()
        {

        }
    }

}
