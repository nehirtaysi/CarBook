using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.CarFeatureDtos;
using UdemyCarBook.Dto.CategoryDtos;
using UdemyCarBook.Dto.FeatureDtos;

namespace UdemyCarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCarFeatureDetail")]
    public class AdminCarFeatureDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminCarFeatureDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // 1. Sistemdeki tüm ana özellikleri havuzdan çekiyoruz
            var responseFeatures = await client.GetAsync("http://nehircarbookapi.somee.com/api/Features");

            // 2. Arabaya ait halihazırda köprü tablosunda oluşmuş olan özellikleri çekiyoruz
            var responseMessage = await client.GetAsync("http://nehircarbookapi.somee.com/api/CarFeatures?id=" + id);

            if (responseMessage.IsSuccessStatusCode && responseFeatures.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);

                var jsonFeaturesData = await responseFeatures.Content.ReadAsStringAsync();
                var allFeatures = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonFeaturesData);

                // Eğer araca ait özellik satır sayısı, havuzdaki toplam özellik sayısından az ise eksik olanları tamamlıyoruz
                if (values.Count < allFeatures.Count)
                {
                    foreach (var feature in allFeatures)
                    {
                        // Eğer bu özellik araca henüz tanımlanmamışsa (köprü tablosunda satırı yoksa)
                        if (!values.Any(x => x.FeatureID == feature.FeatureID))
                        {
                            // Yeni bir araba-özellik ilişkisi modeli oluşturup varsayılanı false yapıyoruz
                            var createCarFeatureDto = new { CarID = id, FeatureID = feature.FeatureID, Available = false };
                            var content = new StringContent(JsonConvert.SerializeObject(createCarFeatureDto), Encoding.UTF8, "application/json");

                            // API tarafındaki CarFeatures post ucuna göndererek satırı veritabanında oluşturuyoruz
                            await client.PostAsync("http://nehircarbookapi.somee.com/api/CarFeatures", content);
                        }
                    }

                    // Eksik olan satırlar API tarafında oluştuktan sonra, güncel CarFeatureID değerlerini almak için listeyi yeniden çekiyoruz
                    var renewedResponse = await client.GetAsync("http://nehircarbookapi.somee.com/api/CarFeatures?id=" + id);
                    if (renewedResponse.IsSuccessStatusCode)
                    {
                        var renewedJson = await renewedResponse.Content.ReadAsStringAsync();
                        values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(renewedJson);
                    }
                }

                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(List<ResultCarFeatureByCarIdDto> resultCarFeatureByCarIdDto)
        {
            foreach (var item in resultCarFeatureByCarIdDto)
            {
                if (item.Available)
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("http://nehircarbookapi.somee.com/api/CarFeatures/CarFeatureChangeAvailableToTrue?id=" + item.CarFeatureID);
                }
                else
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("http://nehircarbookapi.somee.com/api/CarFeatures/CarFeatureChangeAvailableToFalse?id=" + item.CarFeatureID);
                }
            }
            return RedirectToAction("Index", "AdminCar");
        }

        [Route("CreateFeatureByCarId")]
        [HttpGet]
        public async Task<IActionResult> CreateFeatureByCarId()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://nehircarbookapi.somee.com/api/Features");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}