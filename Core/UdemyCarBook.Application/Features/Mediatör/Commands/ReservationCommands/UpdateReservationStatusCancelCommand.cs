using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.ReservationCommands
{
    public class UpdateReservationStatusCancelCommand : IRequest
    {
        public int ReservationID { get; set; }
        public UpdateReservationStatusCancelCommand(int id)
        {
            ReservationID = id;
        }
    }
}