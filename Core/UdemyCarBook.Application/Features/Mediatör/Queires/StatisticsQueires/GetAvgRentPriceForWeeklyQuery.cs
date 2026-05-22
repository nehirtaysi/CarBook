using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.StatisticsResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.StatisticsQueires
{
    public class GetAvgRentPriceForWeeklyQuery : IRequest<GetAvgRentPriceForWeeklyQueryResult>
    {
    }
}
