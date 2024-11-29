using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.InsertDoador
{
    public class InsertDoadorHandler : IRequestHandler<InsertDoadorCommand, ResultViewModel<int>>
    {
        private readonly BloodBankDbContext _db;
        public InsertDoadorHandler(BloodBankDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel<int>> Handle(InsertDoadorCommand request, CancellationToken cancellationToken)
        {
            var isThereAlreadyThisEmail = await _db.Doadores
                .Where(d => d.Email == request.Email)
                .FirstOrDefaultAsync();

            if (isThereAlreadyThisEmail != null)
            {
                return ResultViewModel<int>.Error("Já existe um usuário cadastrado com este e-mail");
            }

            var doador = request.ToEntity();

            await _db.Doadores.AddAsync(doador);

            await _db.SaveChangesAsync();

            return ResultViewModel<int>.Success(doador.Id);
        }
    }
}
