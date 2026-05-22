using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using UdemyCarBook.Application.Interfaces.CarPricingInterfaces;
using UdemyCarBook.Application.ViewModels;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.CarPricingRepositories
{
    public class CarPricingRepository : ICarPricingRepository
    {
        private readonly CarBookContext _context;
        public CarPricingRepository(CarBookContext context)
        {
            _context = context;
        }

        public List<CarPricing> GetCarPricingWithCars()
        {
            return _context.CarPricings.Include(x => x.Car).ThenInclude(y => y.Brand).Include(x => x.Pricing).Where(z => z.PricingID == 2).ToList();
        }

        public List<CarPricing> GetCarPricingWithTimePeriod()
        {
            throw new NotImplementedException();
        }

        public List<CarPricingViewModel> GetCarPricingWithTimePeriod1()
        {
            List<CarPricingViewModel> values = new List<CarPricingViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                    SELECT 
                        c.CarID, b.Name as BrandName, c.Model, c.CoverImageUrl,
                        MAX(CASE WHEN cp.PricingID = 2 THEN cp.Amount END) as DailyAmount,
                        MAX(CASE WHEN cp.PricingID = 2 THEN CAST(cp.IsCampaign AS int) END) as IsDailyCampaign,
                        MAX(CASE WHEN cp.PricingID = 2 THEN cp.CampaignPrice END) as DailyCampaignPrice,
                        
                        MAX(CASE WHEN cp.PricingID = 3 THEN cp.Amount END) as WeeklyAmount,
                        MAX(CASE WHEN cp.PricingID = 3 THEN CAST(cp.IsCampaign AS int) END) as IsWeeklyCampaign,
                        MAX(CASE WHEN cp.PricingID = 3 THEN cp.CampaignPrice END) as WeeklyCampaignPrice,
                        
                        MAX(CASE WHEN cp.PricingID = 1 THEN cp.Amount END) as MonthlyAmount,
                        MAX(CASE WHEN cp.PricingID = 1 THEN CAST(cp.IsCampaign AS int) END) as IsMonthlyCampaign,
                        MAX(CASE WHEN cp.PricingID = 1 THEN cp.CampaignPrice END) as MonthlyCampaignPrice
                    FROM Cars c
                    INNER JOIN Brands b ON c.BrandID = b.BrandID
                    LEFT JOIN CarPricings cp ON c.CarID = cp.CarId
                    GROUP BY c.CarID, b.Name, c.Model, c.CoverImageUrl";

                command.CommandType = CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(new CarPricingViewModel
                        {
                            CarID = Convert.ToInt32(reader["CarID"]),
                            Brand = reader["BrandName"].ToString(),
                            Model = reader["Model"].ToString(),
                            CoverImageUrl = reader["CoverImageUrl"].ToString(),

                            DailyAmount = reader["DailyAmount"] != DBNull.Value ? Convert.ToDecimal(reader["DailyAmount"]) : 0,
                            IsDailyCampaign = reader["IsDailyCampaign"] != DBNull.Value && Convert.ToInt32(reader["IsDailyCampaign"]) == 1,
                            DailyCampaignPrice = reader["DailyCampaignPrice"] != DBNull.Value ? Convert.ToDecimal(reader["DailyCampaignPrice"]) : 0,

                            WeeklyAmount = reader["WeeklyAmount"] != DBNull.Value ? Convert.ToDecimal(reader["WeeklyAmount"]) : 0,
                            IsWeeklyCampaign = reader["IsWeeklyCampaign"] != DBNull.Value && Convert.ToInt32(reader["IsWeeklyCampaign"]) == 1,
                            WeeklyCampaignPrice = reader["WeeklyCampaignPrice"] != DBNull.Value ? Convert.ToDecimal(reader["WeeklyCampaignPrice"]) : 0,

                            MonthlyAmount = reader["MonthlyAmount"] != DBNull.Value ? Convert.ToDecimal(reader["MonthlyAmount"]) : 0,
                            IsMonthlyCampaign = reader["IsMonthlyCampaign"] != DBNull.Value && Convert.ToInt32(reader["IsMonthlyCampaign"]) == 1,
                            MonthlyCampaignPrice = reader["MonthlyCampaignPrice"] != DBNull.Value ? Convert.ToDecimal(reader["MonthlyCampaignPrice"]) : 0
                        });
                    }
                }
                _context.Database.CloseConnection();
                return values;
            }
        }
    }
}