using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Features.Mediator.Results.CarPricingResults
{
    public class GetCarPricingWithTimePeriodQueryResult
    {
        public int CarID { get; set; }
        public string Model { get; set; }
        public decimal DailyAmount { get; set; }
        public decimal WeeklyAmount { get; set; }
        public decimal MonthlyAmount { get; set; }
        public string CoverImageUrl { get; set; }
        public string Brand { get; set; }
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