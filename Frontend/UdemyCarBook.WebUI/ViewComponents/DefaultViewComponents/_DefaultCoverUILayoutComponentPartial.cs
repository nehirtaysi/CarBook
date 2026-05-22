using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyCarBook.Dto.BannerDtos;

namespace UdemyCarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultCoverUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultCoverUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var responseMessage = await client.GetAsync("http://nehirtaysi-001-site1.stempurl.com/api/Banners");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultBannerDtos>>(jsonData);

                    // Eğer values null dönerse projenin patlamaması için boş liste gönderiyoruz
                    return View(values ?? new List<ResultBannerDtos>());
                }
            }
            catch (Exception)
            {
                // API'ye ulaşırken herhangi bir hata (bağlantı kopması vs.) olursa sayfa çökmesin
                return View(new List<ResultBannerDtos>());
            }

            // API durum kodu başarısız (404, 500 vs.) dönerse yine boş liste gönderiyoruz
            return View(new List<ResultBannerDtos>());
        }
    }
}