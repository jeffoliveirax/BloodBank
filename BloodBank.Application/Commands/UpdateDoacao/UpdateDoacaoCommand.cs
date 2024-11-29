using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.UpdateDoacao
{
    public class UpdateDoacaoCommand : IRequest<ResultViewModel>
    {
        public UpdateDoacaoCommand(int id, int doadorId, decimal volume)
        {
            Id = id;
            DoadorId = doadorId;
            Data = DateTime.Now;
            Volume = volume;
        }

        public int Id { get; private set; }
        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; private set; }
    }

}
