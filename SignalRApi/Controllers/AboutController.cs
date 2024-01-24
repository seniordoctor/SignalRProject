using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            // ctor ekleyerek; dependencies injection uyguladik. aboutService bagimliligini azalttik ve aboutService icindeki metodlara erisim sagliyor olabilecegiz.
            _aboutService = aboutService;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var values = _aboutService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            // Create ve Update isleminde about turunde bir entity istiyor.
            About about = new About()
            {
                Title = createAboutDto.Title,
                Description = createAboutDto.Description,
                ImageUrl = createAboutDto.ImageUrl,
            };
            _aboutService.TAdd(about);
            return Ok("Hakkımda kısmı başarılı bir şekilde eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var value = _aboutService.TGetById(id);
            _aboutService.TDelete(value);
            return Ok("Hakkımda alanı silindi.");
        }

        [HttpPut] // guncelleme
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            About about = new About()
            {
                AboutID = updateAboutDto.AboutID,
                ImageUrl = updateAboutDto.ImageUrl,
                Description = updateAboutDto.Description,
                Title = updateAboutDto.Title
            };
            _aboutService.TUpdate(about);
            return Ok("Hakkımda alanı güncellendi.");
        }

        [HttpGet("GetAbout")] // "GetAbout" vermemizin sebebi ayni attribute ayni formatta birden fazla kullanilirsa hata verir.
        public IActionResult GetAbout(int id)
        {
            var value = _aboutService.TGetById(id);
            return Ok(value);
        }
    }
}
