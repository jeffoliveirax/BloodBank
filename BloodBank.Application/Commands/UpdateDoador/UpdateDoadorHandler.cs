using Azure.Core;
using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.UpdateDoador
{
    public class UpdateDoadorHandler : IRequestHandler<UpdateDoadorCommand, ResultViewModel>
    {
        private readonly BloodBankDbContext _db;
        public UpdateDoadorHandler(BloodBankDbContext db)
        {
            _db = db;
        }
        public async Task<ResultViewModel> Handle(UpdateDoadorCommand request, CancellationToken cancellationToken)
        {
            var doador = await _db.Doadores.SingleOrDefaultAsync(d => d.Id == request.Id);

            if (doador is null) return ResultViewModel.Error("Este doador não existe.");

            doador.Update(
                request.Email,
                request.DataNascimento,
                request.Genero,
                request.Peso,
                request.Endereco,
                request.TipoSanguineo,
                request.FatorRh);

            _db.Doadores.Update(doador);
            await _db.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
