using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.FeatureResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.FeatureQueires
{
    public class GetFeatureQuery: IRequest<List<GetFeatureQueryResult>>
    {
    }
}
