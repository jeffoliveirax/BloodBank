using BloodBank.Application.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.DeleteDoador
{
    public class DeleteDoadorHandler : IRequestHandler<DeleteDoadorCommand, ResultViewModel>
    {
        private readonly IDoadorRepository _repository;
        public DeleteDoadorHandler(IDoadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteDoadorCommand request, CancellationToken cancellationToken)
        {
            var existDoador = await _repository.GetById(request.Id);

            if (existDoador is null)
                return ResultViewModel.Error("Este doador não existe.");

            await _repository.Delete(request.Id);

            return ResultViewModel.Success();
        }
    }
}
