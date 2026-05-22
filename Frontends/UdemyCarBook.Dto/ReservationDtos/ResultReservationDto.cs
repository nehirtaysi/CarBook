namespace UdemyCarBook.Dto.ReservationDtos
{
    public class ResultReservationDto
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