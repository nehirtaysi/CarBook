using MediatR;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class UpdateReservationStatusCancelCommandHandler : IRequestHandler<UpdateReservationStatusCancelCommand>
    {
        private readonly IRepository<Reservation> _repository;
        public UpdateReservationStatusCancelCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateReservationStatusCancelCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.ReservationID);
            if (value != null)
            {
                value.Status = "İptal Edildi";
                await _repository.UpdateAsync(value);
            }
        }
    }
}