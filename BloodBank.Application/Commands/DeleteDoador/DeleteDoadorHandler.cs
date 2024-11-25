using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.DeleteDoador
{
    public class DeleteDoadorHandler : IRequestHandler<DeleteDoadorCommand, ResultViewModel>
    {
        private readonly BloodBankDbContext _db;
        public DeleteDoadorHandler(BloodBankDbContext db) => _db = db;
        public async Task<ResultViewModel> Handle(DeleteDoadorCommand request, CancellationToken cancellationToken)
        {
            var doador = await _db.Doadores.SingleOrDefaultAsync(d => d.Id == request.Id);

            if (doador is null)
                return ResultViewModel.Error("Este doador não existe.");

            doador.SetAsDeleted();

            _db.Doadores.Update(doador);
            await _db.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
