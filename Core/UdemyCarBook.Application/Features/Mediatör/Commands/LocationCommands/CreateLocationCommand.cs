using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediatör.Commands.LocationCommand
{
    public class CreateLocationCommand:IRequest
    {
     
        public string Name { get; set; }

    }
}
