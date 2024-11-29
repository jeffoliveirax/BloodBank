using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Query.GetEstoque
{
    public class GetEstoqueAllQuery : IRequest<ResultViewModel<List<Estoque>>>
    {
    }
}
