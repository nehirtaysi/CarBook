using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediatör.Commands.LocationCommand
{
    public class UpdateLocationCommand:IRequest
    {
        public int LocationID { get; set; }
        public string Name { get; set; }

    }
}
