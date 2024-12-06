using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;

namespace BloodBank.Application.Query.GetEstoque
{
    public class GetEstoqueAllHandler : IRequestHandler<GetEstoqueAllQuery, ResultViewModel<List<Estoque>>>
    {
        private readonly IDoacaoRepository _repDonation;
        private readonly IDoadorRepository _repDonor;
        public GetEstoqueAllHandler(IDoacaoRepository repDonation, IDoadorRepository repDonor)
        {
            _repDonation = repDonation;
            _repDonor = repDonor;
        }
        public async Task<ResultViewModel<List<Estoque>>> Handle(GetEstoqueAllQuery request, CancellationToken cancellationToken)
        {
            var donations = await _repDonation.GetAll();
            var donors = await _repDonor.GetAll();

            List<Estoque> estoque = Estoque.CalcularEstoque(donations, donors);

            if (estoque.Count != 0)
            {
                return ResultViewModel<List<Estoque>>.Success(estoque);
            }
            else
            {
                return ResultViewModel<List<Estoque>>.Error("Não há estoque disponível!");
            }
        }
    }
}
