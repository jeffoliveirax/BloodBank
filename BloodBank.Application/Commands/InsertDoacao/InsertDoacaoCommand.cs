using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.InsertDoacao
{
    public class InsertDoacaoCommand : IRequest<ResultViewModel>
    {
        public InsertDoacaoCommand(int doadorId, DateTime data, decimal volume)
        {
            DoadorId = doadorId;
            Data = data;
            Volume = volume;
        }

        public int DoadorId { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Volume { get; set; }
    }
}
