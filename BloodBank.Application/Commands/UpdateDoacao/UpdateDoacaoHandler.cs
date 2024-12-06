using Azure.Core;
using BloodBank.Application.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.UpdateDoacao
{
    public class UpdateDoacaoHandler : IRequestHandler<UpdateDoacaoCommand, ResultViewModel>
    {
        private readonly IDoacaoRepository _repository;
        public UpdateDoacaoHandler(IDoacaoRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateDoacaoCommand request, CancellationToken cancellationToken)
        {
            var doacao = await _repository.GetById(request.Id);

            if (doacao is null)
                return ResultViewModel.Error("A doação não existe.");

            doacao.Update(request.DoadorId, request.Volume);

            await _repository.Update(doacao);

            return ResultViewModel.Success();
        }
    }
}
