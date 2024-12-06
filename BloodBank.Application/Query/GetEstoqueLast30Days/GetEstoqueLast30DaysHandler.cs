using BloodBank.Application.Models;
using BloodBank.Application.Query.GetEstoque;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;

namespace BloodBank.Application.Query.GetEstoqueLast30Days
{
    public class GetEstoqueLast30DaysHandler : IRequestHandler<GetEstoqueLast30DaysQuery, ResultViewModel<List<Estoque>>>
    {
        private readonly IDoacaoRepository _repDonation;
        private readonly IDoadorRepository _repDonor;
        public GetEstoqueLast30DaysHandler(IDoacaoRepository repDonation, IDoadorRepository repDonor)
        {
            _repDonation = repDonation;
            _repDonor = repDonor;
        }
        public async Task<ResultViewModel<List<Estoque>>> Handle(GetEstoqueLast30DaysQuery request, CancellationToken cancellationToken)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);

            var donations = await _repDonation.GetAll();
            var donors = await _repDonor.GetAll();

            var doacoes = donations
                            .Where(d => d.Data >= startDate && d.Data <= endDate)
                            .ToList();

            if (doacoes.Count != 0)
            {
                List<Estoque> estoque = Estoque.CalcularEstoque(doacoes, donors);

                return ResultViewModel<List<Estoque>>.Success(estoque);
            }
            else
            {
                return ResultViewModel<List<Estoque>>.Error("Não há doações no período!");
            }
        }
    }
}
