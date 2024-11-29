using Azure.Core;
using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.UpdateDoacao
{
    public class UpdateDoacaoHandler : IRequestHandler<UpdateDoacaoCommand, ResultViewModel>
    {
        private readonly BloodBankDbContext _db;
        public UpdateDoacaoHandler(BloodBankDbContext db)
        {
            _db = db;
        }
        public async Task<ResultViewModel> Handle(UpdateDoacaoCommand request, CancellationToken cancellationToken)
        {
            var doacao = await _db.Doacoes.SingleOrDefaultAsync(d => d.Id == request.Id);

            if (doacao is null)
                return ResultViewModel.Error("A doação não existe.");

            doacao.Update(request.DoadorId, request.Volume);

            _db.Doacoes.Update(doacao);
            await _db.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
