using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;

namespace BloodBank.Application.Services
{
    public interface IDoadorService
    {
        #region Doadores
        ResultViewModel<List<Doador>> GetAll(int pageNumber = 1, int pageSize = 10);
        ResultViewModel<Doador> GetById(int id);
        ResultViewModel<int> Insert(CreateDoadorInputModel model);
        ResultViewModel Update(int id, UpdateDoadorInputModel model);
        ResultViewModel Delete(int id);
        #endregion
    }

    
}
