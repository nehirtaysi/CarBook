using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationsWithLocationsAsync();
    }
}