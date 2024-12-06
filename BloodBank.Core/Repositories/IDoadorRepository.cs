using BloodBank.Core.Entities;

namespace BloodBank.Core.Repositories
{
    public interface IDoadorRepository
    {
        Task<Doador> GetById(int id);
        Task<int> Insert(Doador doador);
        Task Update(Doador doador);
        Task Delete(int id);
        Task<bool> Exists(string email);
        Task<(List<Doador> Doadores, int TotalItems)> GetAll(int pageNumber, int pageSize);
        Task<List<Doador>> GetAll();
    }
}
