using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.ServiceResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.ServiceQueires
{
    public class GetServiceQuery:IRequest<List<GetServiceQueryResult>>
    {
    }
}
