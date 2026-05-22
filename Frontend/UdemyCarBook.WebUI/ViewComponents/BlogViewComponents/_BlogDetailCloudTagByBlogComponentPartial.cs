using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.BlogDtos;
using UdemyCarBook.Dto.TagCloudDtos;

namespace UdemyCarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailCloudTagByBlogComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _BlogDetailCloudTagByBlogComponentPartial(IHttpClientFactory httpClientFactory)

        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)

        {
            ViewBag.Id = id;

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"http://nehirtaysi-001-site1.stempurl.com/api/TagClouds/GetTagCloudByBlogId?id=" + id);

            if (responseMessage.IsSuccessStatusCode)

            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<GetByBlogIdTagCloudDto>>(jsonData);

                return View(values);
            }

            return View();

        }
    }
}
