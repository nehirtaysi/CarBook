using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace UdemyCarBook.WebUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(IFormFile ImageFile, string Name, string Title, string Comment)
        {
            string finalImageUrl = "";

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(resource, "wwwroot/testimonialimages", imageName);

                if (!Directory.Exists(Path.Combine(resource, "wwwroot/testimonialimages")))
                    Directory.CreateDirectory(Path.Combine(resource, "wwwroot/testimonialimages"));

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                finalImageUrl = "/testimonialimages/" + imageName;
            }
            else
            {
                finalImageUrl = "https://media.istockphoto.com/id/1495088043/tr/vekt%C3%B6r/user-profile-icon-avatar-or-person-icon-profile-picture-portrait-symbol-default-portrait.jpg?s=612x612&w=0&k=20&c=xpdT_tBhcZudUtboWVCTG4cQ9xvc9J-yQ5_dVRPLka4=";
            }

            var client = _httpClientFactory.CreateClient();

            var model = new
            {
                Name = Name,
                Title = Title,
                Comment = Comment,
                ImageUrl = finalImageUrl 
            };

            var jsonData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://nehirtaysi-001-site1.stempurl.com/api/Testimonials", content);

            return RedirectToAction("Index", "Default");
        }
    }
}