using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient(); // istemci olusturduk
			var responseMessage = await client.GetAsync("https://localhost:44363/api/Product/ProductListWithCategory");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // icerigi string formatinda oku, json'dan gelen icin
				// json bir veriyi cozerken Deserialize, json'a veri gonderirken Serialize. Listelerken Deserialize, Ekleme ve guncelleme serialize 
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				return View(values);
			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> CreateProduct()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:44363/api/Category");
			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // Listeleme olacagi icin Deserialize kullaniyoruz.
			List<SelectListItem> values2 = (from x in values
											select new SelectListItem
											{
												Text = x.CategoryName,
												Value = x.CategoryID.ToString()
											}).ToList();    // <SelectListItem> bir deger cekecegimi bildirdim
			ViewBag.v = values2;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			createProductDto.ProductStatus = true;
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:44363/api/Product", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:44363/api/Product/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
		public async Task<IActionResult> UpdateProduct(int id)
		{
            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:44363/api/Category");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1); // Listeleme olacagi icin Deserialize kullaniyoruz.
            List<SelectListItem> values2 = (from x in values1
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryID.ToString()
                                            }).ToList();    // <SelectListItem> bir deger cekecegimi bildirdim
            ViewBag.v = values2;

            var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:44363/api/Product/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
				return View(values);
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:44363/api/Product", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
