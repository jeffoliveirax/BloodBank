using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Query.GetDoadoresAll
{
    public class GetDoadoresAllQuery : IRequest<ResultViewModel<List<Core.Entities.Doador>>>
    {
        public GetDoadoresAllQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
