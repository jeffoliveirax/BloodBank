using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDoador
{
    public class DeleteDoadorCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public DeleteDoadorCommand(int id)
        {
            Id = id;
        }
    }
}
