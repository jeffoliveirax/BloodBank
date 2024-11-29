using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.DeleteDoacao
{
    public class DeleteDoacaoHandler : IRequestHandler<DeleteDoacaoCommand, ResultViewModel>
    {
        private readonly BloodBankDbContext _db;
        public DeleteDoacaoHandler(BloodBankDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel> Handle(DeleteDoacaoCommand request, CancellationToken cancellationToken)
        {
            var doacao = await _db.Doacoes.SingleOrDefaultAsync(d => d.Id == request.Id);

            if (doacao is null || doacao.IsDeleted)
                return ResultViewModel.Error("A doação não existe.");

            doacao.SetAsDeleted();

            _db.Doacoes.Update(doacao);
            await _db.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
