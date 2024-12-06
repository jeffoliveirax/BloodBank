using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Commands.InsertDoacao
{
    public class InsertDoacaoHandler : IRequestHandler<InsertDoacaoCommand, ResultViewModel<int>>
    {
        private readonly IDoacaoRepository _repDonation;
        private readonly IDoadorRepository _repDonor;
        public InsertDoacaoHandler(IDoacaoRepository repDonation, IDoadorRepository repDonor)
        {
            _repDonation = repDonation;
            _repDonor = repDonor;
        }
        public async Task<ResultViewModel<int>> Handle(InsertDoacaoCommand request, CancellationToken cancellationToken)
        {
            var doador = await _repDonor.GetById(request.DoadorId);
            if (doador == null)
            {
                return ResultViewModel<int>.Error("Id de doador não existe!");
            }

            var idade = DateTime.Today.Year - doador.DataNascimento.Year;
            if (doador.DataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < 18)
            {
                return ResultViewModel<int>.Error("O doador deve ter mais de 18 anos para efetuar uma doação.");
            }

            if (doador.Peso < 50)
            {
                return ResultViewModel<int>.Error("O doador deve pesar no mínimo 50kg para efetuar uma doação.");
            }

            if (doador.Genero.ToLower() == "feminino")
            {
                var ultimaDoacao = await _repDonation.GetByDoador(doador.Id);
                
                if (ultimaDoacao != null && (DateTime.Today - ultimaDoacao.Data).TotalDays < 90)
                {
                    return ResultViewModel<int>.Error("Mulheres só podem doar de 90 em 90 dias.");
                }
            }

            if (doador.Genero.ToLower() == "masculino")
            {
                var ultimaDoacao = await _repDonation.GetByDoador(doador.Id);

                if (ultimaDoacao != null && (DateTime.Today - ultimaDoacao.Data).TotalDays < 60)
                {
                    return ResultViewModel<int>.Error("Homens só podem doar de 60 em 60 dias.");
                }
            }

            if (request.Volume < 420 || request.Volume > 470)
            {
                return ResultViewModel<int>.Error("A quantidade de mililitros de sangue doados deve ser entre 420ml e 470ml.");
            }

            var doacao = request.ToEntity();

            await _repDonation.Insert(doacao);

            return ResultViewModel<int>.Success(doacao.Id);
        }
    }
}
