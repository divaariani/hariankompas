using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

public class BookFormatController : Controller
{
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookFormatViewModel model)
    {
        if (ModelState.IsValid)
        {
            var bookFormat = new BookFormat
            {
                Id = 0,
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                Token = "hariankompas"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8000/");
                var content = new StringContent(JsonConvert.SerializeObject(bookFormat), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/BookFormat/Save", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Server error: {response.StatusCode}. Response: {responseContent}");
                }
            }
        }

        return View(model);
    }
}