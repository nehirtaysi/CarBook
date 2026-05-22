using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.EmailInterfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IEmailService _emailService;

        public CreateReservationCommandHandler(IRepository<Reservation> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var rentalDuration = request.DropOffDate - request.PickUpDate;
            if (rentalDuration.TotalDays > 30)
            {
                throw new Exception("Bir araç tek seferde maksimum 30 gün kiralanabilir.");
            }

            var reservations = await _repository.GetAllAsync();

            var isBusy = reservations.Any(x =>
                x.CarID == request.CarID &&
                x.Status != "İptal Edildi" &&
                (request.PickUpDate < x.DropOffDate.AddHours(2) && request.DropOffDate > x.PickUpDate)
            );

            if (isBusy)
            {
                throw new Exception("Seçtiğiniz araç bu tarihler arasında müsait değildir.");
            }

            decimal fakeUserBalance = 10000.00m;

            if (request.TotalPrice > fakeUserBalance)
            {
                throw new Exception("Bakiyeniz yeterli değildir, lütfen bankanız ile iletişime geçiniz.");
            }

            var values = new Reservation
            {
                Age = request.Age,
                CarID = request.CarID,
                Description = request.Description,
                DriverLicenseYear = request.DriverLicenseYear,
                DropOffLocationID = request.DropOffLocationID,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                PickUpLocationID = request.PickUpLocationID,
                Surname = request.Surname,
                Status = "Ödeme Alındı - Rezervasyon Onaylandı",
                PickUpDate = request.PickUpDate,
                DropOffDate = request.DropOffDate
            };

            await _repository.CreateAsync(values);

            string subject = "CarBook Rezervasyon ve Ödeme Onayı";

            string lastFourDigits = request.CardNumber.Length >= 4
                ? request.CardNumber.Substring(request.CardNumber.Length - 4)
                : "****";

            string body = $@"
            <div style='font-family: Arial, sans-serif; border: 1px solid #eee; padding: 20px; border-radius: 10px;'>
                <h2 style='color: #1089ff;'>Ödemeniz Onaylandı!</h2>
                <p>Sayın <b>{request.Name} {request.Surname}</b>,</p>
                <hr>
                <p><b>Sürücü Belgesi No:</b> {request.LicenseNumber}</p>
                <p><b>Ödeme Kartı:</b> **** **** **** {lastFourDigits}</p>
                <hr>
                <p><b>Alış Tarihi:</b> {request.PickUpDate:dd.MM.yyyy HH:mm}</p>
                <p><b>İade Tarihi:</b> {request.DropOffDate:dd.MM.yyyy HH:mm}</p>
                <hr>
                <p>İptal işlemleriniz için aşağıdaki butonu kullanabilirsiniz:</p>
<a href='http://nehirtaysi-001-site1.stempurl.com/api/Reservations/ChangeReservationStatusToCancel/{values.ReservationID}' 
   style='display:inline-block; padding:10px 20px; background-color:#dc3545; color:#ffffff; text-decoration:none; border-radius:5px;'>
   Rezervasyonu İptal Et
</a>
            </div>";

            await _emailService.SendReservationEmailAsync(request.Email, subject, body);
        }
    }
}