using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace hariankompas.Pages
{
    public class BookDetailModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public BookDetailModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public BookRow Row { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:8000/api/BookFormat/Read?ID={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BookData>(jsonString);

                if (data.Rows.Count > 0)
                {
                    Row = data.Rows[0];
                    return Page();
                }
            }

            return RedirectToPage("/Index");
        }
    }
}