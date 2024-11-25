using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Persistence;

namespace BloodBank.Application.Services
{
    public interface IEstoqueService
    {
        public ResultViewModel<List<Estoque>> GetAll();

        public ResultViewModel<List<Estoque>> GetByDate();

    }
}
