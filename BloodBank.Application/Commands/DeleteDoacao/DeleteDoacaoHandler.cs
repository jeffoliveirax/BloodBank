using BloodBank.Application.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDoacao
{
    public class DeleteDoacaoHandler : IRequestHandler<DeleteDoacaoCommand, ResultViewModel>
    {
        private readonly IDoacaoRepository _repository;
        public DeleteDoacaoHandler(IDoacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteDoacaoCommand request, CancellationToken cancellationToken)
        {
            var doacao = await _repository.Exists(request.Id);

            if (!doacao)
                return ResultViewModel.Error("A doação não existe.");

            await _repository.Delete(request.Id);

            return ResultViewModel.Success();
        }
    }
}
