using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Query.GetDoadorById
{
    public class GetDoadorByIdHandler : IRequestHandler<GetDoadorByIdQuery, ResultViewModel<Core.Entities.Doador>>
    {
        private readonly IDoadorRepository _repository;
        public GetDoadorByIdHandler(IDoadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<Doador>> Handle(GetDoadorByIdQuery request, CancellationToken cancellationToken)
        {
            var doador = await _repository.GetById(request.Id);

            if (doador is null)
                return ResultViewModel<Doador>.Error("Não há doador cadastrado com esse Id.");

            return ResultViewModel<Doador>.Success(doador);
        }
    }
}
