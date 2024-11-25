using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDoacao
{
    public class DeleteDoacaoCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DeleteDoacaoCommand(int id)
        {
            Id = id;
        }
    }
}
