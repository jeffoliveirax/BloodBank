using BloodBank.Application.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Query.GetDoacaoById
{
    public class GetDoacaoByIdHandler : IRequestHandler<GetDoacaoByIdQuery, ResultViewModel<DoacaoViewModel>>
    {
        private readonly IDoacaoRepository _repository;
        public GetDoacaoByIdHandler(IDoacaoRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<DoacaoViewModel>> Handle(GetDoacaoByIdQuery request, CancellationToken cancellationToken)
        {
            var doacao = await _repository.GetById(request.Id);

            if (doacao is null)
                return ResultViewModel<DoacaoViewModel>.Error("Doação não existe.");

            var model = DoacaoViewModel.FromEntity(doacao);

            return ResultViewModel<DoacaoViewModel>.Success(model);
        }
    }
}
