using BloodBank.API.Entities;

namespace BloodBank.API.Models
{
    public class CreateDoacaoInputModel
    {
        public Doador Doador { get; set; }
        public decimal Volume { get; set; }

    }
}
