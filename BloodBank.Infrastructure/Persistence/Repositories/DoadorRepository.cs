using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories
{
    public class DoadorRepository : IDoadorRepository
    {
        private readonly BloodBankDbContext _db;
        public DoadorRepository(BloodBankDbContext db) => _db = db;
        public async Task Delete(int id)
        {
            var doador = await _db.Doadores
                                  .SingleOrDefaultAsync(d => d.Id == id);

            doador.SetAsDeleted();

            _db.Doadores.Update(doador);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Exists(string email)
        {
            return await _db.Doadores
                            .Where(d => !d.IsDeleted)
                            .AnyAsync(d => d.Email == email);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Doadores
                            .Where(d => !d.IsDeleted)
                            .AnyAsync(d => d.Id == id);
        }

        public async Task<Doador> GetById(int id)
        {
            return await _db.Doadores.Where(d => !d.IsDeleted)
                                     .SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Insert(Doador doador)
        {
            await _db.Doadores.AddAsync(doador);
            await _db.SaveChangesAsync();

            return doador.Id;
        }

        public async Task Update(Doador doador)
        {
            doador.Update(
                doador.Email,
                doador.DataNascimento,
                doador.Genero,
                doador.Peso,
                doador.Endereco,
                doador.TipoSanguineo,
                doador.FatorRh);

            _db.Doadores.Update(doador);
            await _db.SaveChangesAsync();
        }

        public async Task<(List<Doador> Doadores, int TotalItems)> GetAll(int pageNumber, int pageSize)
        {
            var doadoresNotDeleted = await _db.Doadores
                                      .Where(d => !d.IsDeleted)
                                      .ToListAsync();

            var totalItems = doadoresNotDeleted.Count();

            var doadores = doadoresNotDeleted
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return (doadores, totalItems);
        }

        public async Task<List<Doador>> GetAll()
        {
            return await _db.Doadores
                            .Where(d => !d.IsDeleted)
                            .ToListAsync();
        }
    }
}
