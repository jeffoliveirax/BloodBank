using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Query.GetDoacaoById
{
    public class GetDoacaoByIdQuery : IRequest<ResultViewModel<DoacaoViewModel>>
    {
        public GetDoacaoByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

    }
}
