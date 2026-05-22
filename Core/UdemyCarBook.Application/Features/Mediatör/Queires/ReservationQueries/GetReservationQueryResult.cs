namespace UdemyCarBook.Application.Features.Mediator.Results.ReservationResults
{
    public class GetReservationQueryResult
    {
        public int ReservationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int PickUpLocationID { get; set; }
        public int DropOffLocationID { get; set; }
        public string Status { get; set; }
        public string PickUpLocationName { get; set; }
        public string DropOffLocationName { get; set; }
    }
}