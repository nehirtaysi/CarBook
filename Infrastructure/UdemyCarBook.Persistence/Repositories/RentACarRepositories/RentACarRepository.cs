using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.RentACarRepositories
{
    public class RentACarRepository : IRentACarRepository
    {
        private readonly CarBookContext _context;
        public RentACarRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<List<RentACar>> GetByFilterAsync(Expression<Func<RentACar, bool>> filter)
        {
            return await _context.RentACars
                .Where(filter)
                .Include(x => x.Car).ThenInclude(y => y.Brand)
                .Include(x => x.Car).ThenInclude(y => y.CarPricings)
                .ToListAsync();
        }

        public async Task<List<RentACar>> GetAvailableRentACarsByFilterAsync(int locationId, DateTime pickDate, DateTime offDate)
        {
            var allReservations = await _context.Reservations
                .Where(x => x.Status != "İptal Edildi")
                .ToListAsync();

            var busyCarIds = allReservations
                .Where(x => pickDate < x.DropOffDate.AddHours(2) && offDate > x.PickUpDate)
                .Select(x => x.CarID)
                .ToList();

            return await _context.RentACars
                .Where(x => x.LocationID == locationId && x.Available == true && !busyCarIds.Contains(x.CarID))
                .Include(x => x.Car).ThenInclude(y => y.Brand)
                .Include(x => x.Car).ThenInclude(y => y.CarPricings)
                .ToListAsync();
        }
    }
}