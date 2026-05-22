using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.FooterAddressResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.FooterAddressQueires
{
    public class GetFooterAddressQuery: IRequest<List<GetFooterAddressQueryResult>>
    {
    }
}
