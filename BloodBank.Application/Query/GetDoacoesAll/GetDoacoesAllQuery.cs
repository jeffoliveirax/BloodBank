using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Query.GetDoacoesAll
{
    public class GetDoacoesAllQuery : IRequest<ResultViewModel<List<DoacoesItemViewModel>>>
    {
    }
}
