using BloodBank.Application.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.UpdateDoador
{
    public class UpdateDoadorHandler : IRequestHandler<UpdateDoadorCommand, ResultViewModel>
    {
        private readonly IDoadorRepository _repository;
        public UpdateDoadorHandler(IDoadorRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateDoadorCommand request, CancellationToken cancellationToken)
        {
            var doador = await _repository.GetById(request.Id);

            if (doador is null) return ResultViewModel.Error("Este doador não existe.");

            doador.Update(
                request.Email,
                request.DataNascimento,
                request.Genero,
                request.Peso,
                request.Endereco,
                request.TipoSanguineo,
                request.FatorRh);

            await _repository.Update(doador);

            return ResultViewModel.Success();
        }
    }
}
