using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Query.GetEstoqueLast30Days
{
    public class GetEstoqueLast30DaysQuery : IRequest<ResultViewModel<List<Estoque>>>
    {
    }
}
