using BloodBank.Application.Models;
using BloodBank.Core.Entities;

namespace BloodBank.Application.Services
{
    public interface IDoacaoService
    {
        #region Doacoes
        ResultViewModel<List<DoacoesItemViewModel>> GetAll(string search = "");
        ResultViewModel<DoacaoViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateDoacaoInputModel model);
        ResultViewModel Update(int id, UpdateDoacaoInputModel model);
        ResultViewModel Delete(int id);
        #endregion
    }
}
