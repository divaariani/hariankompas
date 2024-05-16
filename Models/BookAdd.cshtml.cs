using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace hariankompas.Pages;

public class BookAddModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public BookAddModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task OnGet()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:8000/api/BookFormat/List?page=1&pageSize=20");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BookAddData>(jsonString);

            ViewData["Rows"] = data.Rows;
        }
        else
        {
            // Handle if the request failed
        }
    }

}

public class BookAddData
{
    public List<BookAddRow>? Rows { get; set; }
    public int Status { get; set; }
    public string? Message { get; set; }
    public string? MessageDescription { get; set; }
    public int RowCount { get; set; }
    public List<string>? Columns { get; set; }
}

public class BookAddRow
{
    public int RowNumber { get; set; }
    public int RowCount { get; set; }
    public int ID { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int ModifiedBy { get; set; }
}