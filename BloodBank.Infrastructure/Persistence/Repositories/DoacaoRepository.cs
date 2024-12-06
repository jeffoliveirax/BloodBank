using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories
{

    public class DoacaoRepository : IDoacaoRepository
    {
        public readonly BloodBankDbContext _db;
        public DoacaoRepository(BloodBankDbContext db)
        {
            _db = db;
        }
        public async Task Delete(int id)
        {
            var doacao = await _db.Doacoes
                                .SingleOrDefaultAsync(d => d.Id == id);

            doacao.SetAsDeleted();

            _db.Doacoes.Update(doacao);

            await _db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Doacoes
                            .Where(d => !d.IsDeleted)
                            .AnyAsync(d => d.Id == id);
        }

        public async Task<List<Doacao>> GetAll()
        {
            return await _db.Doacoes
                           .Where(d => !d.IsDeleted)
                           .Include(d => d.Doador)
                           .ToListAsync();
        }

        public async Task<Doacao> GetById(int id)
        {
           return await _db.Doacoes
                            .Where(d => !d.IsDeleted)
                            .Include(d => d.Doador)
                            .SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Insert(Doacao doacao)
        {
            await _db.Doacoes.AddAsync(doacao);

            await _db.SaveChangesAsync();

            return doacao.Id;
        }

        public async Task Update(Doacao doacao)
        {
            _db.Doacoes.Update(doacao);
            await _db.SaveChangesAsync();
        }

        public async Task<Doacao> GetByDoador(int id)
        {
            var ultimaDoacao = await _db.Doacoes
                    .Where(d => d.DoadorId == id && !d.IsDeleted)
                    .OrderByDescending(d => d.Data)
                    .FirstOrDefaultAsync();

            return ultimaDoacao;
        }
    }
}
