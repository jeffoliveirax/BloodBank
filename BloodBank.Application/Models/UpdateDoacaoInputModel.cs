namespace BloodBank.Application.Models
{
    public class UpdateDoacaoInputModel
    {
        public UpdateDoacaoInputModel(int doadorId, decimal volume)
        {
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; private set; }
    }
}
