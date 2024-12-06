using BloodBank.Core.Entities;

namespace BloodBank.Core.Repositories
{
    public interface IDoacaoRepository
    {
        Task<Doacao> GetById(int id);
        Task<int> Insert(Doacao doacao);
        Task Update(Doacao doacao);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<List<Doacao>> GetAll();
        Task<Doacao> GetByDoador(int id);
    }
}
