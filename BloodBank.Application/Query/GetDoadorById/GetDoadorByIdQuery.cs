using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enum;
using MediatR;

namespace BloodBank.Application.Query.GetDoadorById
{
    public class GetDoadorByIdQuery : IRequest<ResultViewModel<Doador>>
    {
        public GetDoadorByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
