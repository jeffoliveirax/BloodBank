using BloodBank.Application.Models;
using BloodBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Application.Services
{
    public class DoacaoService : IDoacaoService
    {
        private readonly BloodBankDbContext _db;
        public DoacaoService(BloodBankDbContext db)
        {
            _db = db;
        }
        public ResultViewModel Delete(int id)
        {
            var doacao = _db.Doacoes.SingleOrDefault(d => d.Id == id);

            if (doacao is null)
                return ResultViewModel.Error("A doação não existe.");

            doacao.SetAsDeleted();

            _db.Doacoes.Update(doacao);
            _db.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<DoacoesItemViewModel>> GetAll(string search = "")
        {
            var doacoes = _db.Doacoes
               .Include(d => d.Doador)
               .ToList();

            if (doacoes is null)
                ResultViewModel<List<DoacoesItemViewModel>>.Error("Não existem doações.");

            var model = doacoes.Select(DoacoesItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<DoacoesItemViewModel>>.Success(model);
        }

        public ResultViewModel<DoacaoViewModel> GetById(int id)
        {
            var doacoes = _db.Doacoes
               .Include(d => d.Doador)
               .SingleOrDefault(d => d.Id == id);

            if (doacoes is null)
                return ResultViewModel<DoacaoViewModel>.Error("Doação não existe.");

            var model = DoacaoViewModel.FromEntity(doacoes);

            return ResultViewModel<DoacaoViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateDoacaoInputModel model)
        {
            var doador = _db.Doadores.SingleOrDefault(d => d.Id == model.DoadorId);
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
                var ultimaDoacao = _db.Doacoes
                    .Where(d => d.DoadorId == doador.Id)
                    .OrderByDescending(d => d.Data)
                    .FirstOrDefault();

                if (ultimaDoacao != null && (DateTime.Today - ultimaDoacao.Data).TotalDays < 90)
                {
                    return ResultViewModel<int>.Error("Mulheres só podem doar de 90 em 90 dias.");
                }
            }

            if (doador.Genero.ToLower() == "masculino")
            {
                var ultimaDoacao = _db.Doacoes
                    .Where(d => d.DoadorId == doador.Id)
                    .OrderByDescending(d => d.Data)
                    .FirstOrDefault();

                if (ultimaDoacao != null && (DateTime.Today - ultimaDoacao.Data).TotalDays < 60)
                {
                    return ResultViewModel<int>.Error("Homens só podem doar de 60 em 60 dias.");
                }
            }

            if (model.Volume < 420 || model.Volume > 470)
            {
                return ResultViewModel<int>.Error("A quantidade de mililitros de sangue doados deve ser entre 420ml e 470ml.");
            }

            var doacao = model.ToEntity();
            _db.Doacoes.Add(doacao);
            _db.SaveChanges();

            return ResultViewModel<int>.Success(doacao.Id);
        }

        public ResultViewModel Update(int id, UpdateDoacaoInputModel model)
        {
            var doacao = _db.Doacoes.SingleOrDefault(d => d.Id == id);

            if (doacao is null)
                return ResultViewModel.Error("A doação não existe.");

            doacao.Update(model.DoadorId, model.Volume);

            _db.Doacoes.Update(doacao);
            _db.SaveChanges();

            return ResultViewModel.Success();
        }
    }

}