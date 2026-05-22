using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UdemyCarBook.Dto.BrandDtos;
using UdemyCarBook.Dto.RentACarDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    public class RentACarListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RentACarListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var locationID = TempData["LocationID"];
            var bookPickDate = TempData["bookpickdate"];
            var bookOffDate = TempData["bookoffdate"];
            var timePick = TempData["timepick"];
            var timeOff = TempData["timeoff"];
            TempData.Keep();

            // 1. Lokasyon ID'sini belirliyoruz (Haritadan gelirse id dolu olur)
            int finalLocationId = id > 0 ? id : (locationID != null ? int.Parse(locationID.ToString()) : 0);

            if (finalLocationId == 0)
            {
                return RedirectToAction("Index", "Default");
            }

            // 2. KRİTİK DÜZELTME: Tarihler null ise "T" harfinin tek başına kalmasını engelliyoruz
            string pickDateQuery = (bookPickDate != null && timePick != null) ? $"{bookPickDate}T{timePick}" : "";
            string offDateQuery = (bookOffDate != null && timeOff != null) ? $"{bookOffDate}T{timeOff}" : "";

            ViewBag.locationID = finalLocationId;
            ViewBag.bookPickDate = bookPickDate;
            ViewBag.bookOffDate = bookOffDate;
            ViewBag.timePick = timePick;
            ViewBag.timeOff = timeOff;

            var client = _httpClientFactory.CreateClient();

            // 3. URL'i dinamik oluşturuyoruz. Tarih seçilmemişse (Haritadan gelinmişse) API'ye hatalı tarih yollamıyoruz.
            var url = $"http://nehirtaysi-001-site1.stempurl.com/api/RentACars?locationID={finalLocationId}&available=true";

            if (!string.IsNullOrEmpty(pickDateQuery) && !string.IsNullOrEmpty(offDateQuery))
            {
                url += $"&pickDate={pickDateQuery}&offDate={offDateQuery}";
            }

            var responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FilterRentACarDto>>(jsonData);
                return View(values);
            }

            return View(new List<FilterRentACarDto>());
        }
    }
}