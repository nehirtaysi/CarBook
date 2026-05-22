using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.ReservationQueries;
using UdemyCarBook.Application.Features.Mediator.Results.ReservationResults;
using UdemyCarBook.Application.Interfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, List<GetReservationQueryResult>>
    {
        // KRİTİK DEĞİŞİKLİK: IRepository yerine kendi yazdığımız IReservationRepository'i çağırıyoruz
        private readonly IReservationRepository _repository;

        public GetReservationQueryHandler(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetReservationQueryResult>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            // Yeni metodumuzla lokasyonları dahil (Include) ederek çekiyoruz
            var values = await _repository.GetReservationsWithLocationsAsync();

            return values.Select(x => new GetReservationQueryResult
            {
                ReservationID = x.ReservationID,
                Name = x.Name,
                Surname = x.Surname,
                Phone = x.Phone,
                // Artık ID yerine SQL'den gelen tablo isimlerini basıyoruz
                PickUpLocationName = x.PickUpLocation?.Name ?? "Belirtilmedi",
                DropOffLocationName = x.DropOffLocation?.Name ?? "Belirtilmedi",
                Status = x.Status
            }).ToList();
        }
    }
}