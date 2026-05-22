using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.LocationResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.Location_Queires
{
    public class GetLocationByIdQuery:IRequest<GetLocationByIdQueryResult>
    {
        public int Id  { get; set; }

        public GetLocationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
