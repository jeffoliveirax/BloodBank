using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.InsertDoador
{
    public class InsertDoadorHandler : IRequestHandler<InsertDoadorCommand, ResultViewModel<int>>
    {
        private readonly IDoadorRepository _repository;
        public InsertDoadorHandler(IDoadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertDoadorCommand request, CancellationToken cancellationToken)
        {
            var existEmail = await _repository.Exists(request.Email);

            if (existEmail)
            {
                return ResultViewModel<int>.Error("Já existe um usuário cadastrado com este e-mail");
            }

            var doador = request.ToEntity();

            await _repository.Insert(doador);

            return ResultViewModel<int>.Success(doador.Id);
        }
    }
}
