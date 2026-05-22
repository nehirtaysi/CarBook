using MediatR;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;
using System.Collections.Generic;

namespace UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries
{
    // Veritabanından bir liste döneceğini MediatR'a söylüyoruz
    public class GetReservationQuery : IRequest<List<GetReservationQueryResult>>
    {
    }
}