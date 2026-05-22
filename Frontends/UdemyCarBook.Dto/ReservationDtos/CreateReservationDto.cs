using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.ReservationDtos
{
    public class CreateReservationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PickUpLocationID { get; set; }
        public int DropOffLocationID { get; set; }
        public int CarID { get; set; }
        public int Age { get; set; }
        public int DriverLicenseYear { get; set; }
        public string? Description { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string LicenseNumber { get; set; } 
        public string CardNumber { get; set; }    
        public string CardHolderName { get; set; } 
        public decimal TotalPrice { get; set; }
        public string ExpiryDate { get; set; }
        public string CVC { get; set; }

    }
}