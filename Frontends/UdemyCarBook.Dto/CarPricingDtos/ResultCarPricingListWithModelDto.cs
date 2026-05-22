using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.CarPricingDtos
{
    public class ResultCarPricingListWithModelDto
    {
        public string model { get; set; }
        public decimal dailyAmount { get; set; }
        public decimal weeklyAmount { get; set; }
        public decimal monthlyAmount { get; set; }
        public string CoverImageUrl { get; set; }
        public string Brand { get; set; }
        public int CarID { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }

        public bool IsCampaign { get; set; } 
        public decimal? CampaignPrice { get; set; } 
        public DateTime? CampaignExpiryDate { get; set; }

        public bool IsDailyCampaign { get; set; }
        public decimal? DailyCampaignPrice { get; set; }

        public bool IsWeeklyCampaign { get; set; }
        public decimal? WeeklyCampaignPrice { get; set; }

 
        public bool IsMonthlyCampaign { get; set; }
        public decimal? MonthlyCampaignPrice { get; set; }
    }

}

