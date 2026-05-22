using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context; // Kendi DbContext yolunu kontrol et

namespace UdemyCarBook.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CarBookContext _context;

        public ReservationRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservationsWithLocationsAsync()
        {
            // Include komutları ile SQL'de INNER JOIN yapıyoruz
            return await _context.Reservations
                .Include(x => x.PickUpLocation)
                .Include(x => x.DropOffLocation)
                .ToListAsync();
        }
    }
}