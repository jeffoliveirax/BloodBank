using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;

namespace BloodBank.Application.Services
{
    public class DoadorService : IDoadorService
    {
        private readonly BloodBankDbContext _db;
        public DoadorService(BloodBankDbContext db) => _db = db;

        public ResultViewModel<int> Insert(CreateDoadorInputModel model)
        {
            var isThereAlreadyThisEmail = _db.Doadores.Where(d => d.Email == model.Email).FirstOrDefault();
            if (isThereAlreadyThisEmail != null)
            {
                return ResultViewModel<int>.Error("Já existe um usuário cadastrado com este e-mail");
            }

            var doador = new Doador(
                model.NomeCompleto,
                model.Email,
                model.DataNascimento,
                model.Genero,
                model.Peso,
                model.Endereco,
                model.TipoSanguineo,
                model.FatorRh);

            _db.Doadores.Add(doador);

            _db.SaveChanges();

            return ResultViewModel<int>.Success(doador.Id);
        }

        public ResultViewModel<List<Doador>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var totalItems = _db.Doadores.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var doadores = _db.Doadores
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            if (doadores is null)
                return ResultViewModel<List<Doador>>.Error("Não há doadores cadastrados.");

            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = doadores
            };

            return ResultViewModel<List<Doador>>.Success(response.Data);
        }

        public ResultViewModel<Doador> GetById(int id)
        {
            var doador = _db.Doadores.Where(d => d.Id == id).FirstOrDefault();

            if (doador is null)
                return ResultViewModel<Doador>.Error("Não há doador cadastrado com esse Id.");

            return ResultViewModel<Doador>.Success(doador);
        }

        public ResultViewModel Update(int id, UpdateDoadorInputModel model)
        {
            var doador = _db.Doadores.SingleOrDefault(d => d.Id == id);

            if (doador is null) return ResultViewModel.Error("Este doador não existe.");

            doador.Update(
                model.Email,
                model.DataNascimento,
                model.Genero,
                model.Peso,
                model.Endereco,
                model.TipoSanguineo,
                model.FatorRh);

            _db.Doadores.Update(doador);
            _db.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var doador = _db.Doadores.SingleOrDefault(d => d.Id == id);

            if (doador is null)
                return ResultViewModel.Error("Este doador não existe.");

            doador.SetAsDeleted();

            _db.Doadores.Update(doador);
            _db.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
