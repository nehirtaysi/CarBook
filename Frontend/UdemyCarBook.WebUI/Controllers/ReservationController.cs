using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.Dto.ReservationDtos;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Net.Http; 

namespace UdemyCarBook.WebUI.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var locationID = TempData["LocationID"];
            var pickDate = TempData["bookpickdate"];
            var offDate = TempData["bookoffdate"];
            var timePick = TempData["timepick"];
            var timeOff = TempData["timeoff"];

            if (pickDate != null && timePick != null)
            {
                DateTime d1 = Convert.ToDateTime(pickDate);
                ViewBag.pickDateFull = d1.ToString("yyyy-MM-dd") + "T" + timePick;

                if (offDate != null && timeOff != null)
                {
                    DateTime d2 = Convert.ToDateTime(offDate);
                    ViewBag.offDateFull = d2.ToString("yyyy-MM-dd") + "T" + timeOff;
                }
                ViewBag.locationID = locationID;
            }
            else
            {
                ViewBag.pickDateFull = null;
                ViewBag.offDateFull = null;
                ViewBag.locationID = null;
            }

            ViewBag.v1 = "Rezervasyon Formu";
            ViewBag.v3 = id;

            var client = _httpClientFactory.CreateClient();

            var responseCar = await client.GetAsync($"http://nehirtaysi-001-site1.stempurl.com/api/Cars/{id}");
            if (responseCar.IsSuccessStatusCode)
            {
                var jsonCar = await responseCar.Content.ReadAsStringAsync();
                var carValues = JsonConvert.DeserializeObject<dynamic>(jsonCar);
                ViewBag.CarModelName = carValues.brand?.name + " " + carValues.model;

                string rawPrice = (carValues.price ?? carValues.Price ?? carValues.dailyPrice ?? "0").ToString();
                decimal dailyPriceValue = decimal.Parse(rawPrice, CultureInfo.InvariantCulture);
                ViewBag.DailyPrice = dailyPriceValue.ToString("F2", CultureInfo.InvariantCulture);
            }

            var responseLoc = await client.GetAsync("http://nehirtaysi-001-site1.stempurl.com/api/Locations");
            var jsonLoc = await responseLoc.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonLoc);

            ViewBag.v = locations.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.LocationID.ToString(),
                Selected = (ViewBag.locationID != null && x.LocationID.ToString() == ViewBag.locationID.ToString())
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createReservationDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://nehirtaysi-001-site1.stempurl.com/api/Reservations", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }

            ViewBag.ErrorMessage = "Seçtiğiniz araç bu tarihler arasında müsait değildir.";
            ViewBag.v3 = createReservationDto.CarID;

            return View(createReservationDto);
        }
    }
}