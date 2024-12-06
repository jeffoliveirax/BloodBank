using BloodBank.Application.Models;
using BloodBank.Core.Repositories;
using MediatR;
using System.Linq;

namespace BloodBank.Application.Query.GetDoacoesAll
{
    public class GetDoacoesAllHandler : IRequestHandler<GetDoacoesAllQuery, ResultViewModel<List<DoacoesItemViewModel>>>
    {
        private readonly IDoacaoRepository _repository;
        public GetDoacoesAllHandler(IDoacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<DoacoesItemViewModel>>> Handle(GetDoacoesAllQuery request, CancellationToken cancellationToken)
        {
            var doacoes = await _repository.GetAll();

            if (doacoes == null)
                return ResultViewModel<List<DoacoesItemViewModel>>.Error("Não existem doações.");

            var model = doacoes.Select(DoacoesItemViewModel.ToEntity).ToList();

            return ResultViewModel<List<DoacoesItemViewModel>>.Success(model);
        }
    }
}
