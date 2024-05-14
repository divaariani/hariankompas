using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace hariankompas.Pages;

public class HomeModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public HomeModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task OnGet()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:8000/api/Opname/List?WarehouseID=-1&WorkstateID=-1&Page=1&PageSize=25");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OpnameData>(jsonString);

            ViewData["Rows"] = data.Rows; // Populate ViewData with the deserialized rows
        }
        else
        {
            // Handle if the request failed
        }
    }

}

public class OpnameData
{
    public List<OpnameRow>? Rows { get; set; }
    public int Status { get; set; }
    public string? Message { get; set; }
    public string? MessageDescription { get; set; }
    public int RowCount { get; set; }
    public List<string>? Columns { get; set; }
}

public class OpnameRow
{
    public int RowNumber { get; set; }
    public int RowCount { get; set; }
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime OpnameDate { get; set; }
    public int WarehouseID { get; set; }
    public string? WarehouseName { get; set; }
    public int WorkstateID { get; set; }
    public string? WorkstateName { get; set; }
    public string? WorkstateColor { get; set; }
    public int Confirmed { get; set; }
    public DateTime ConfirmedDate { get; set; }
    public int ConfirmedByID { get; set; }
    public string? ConfirmedByName { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int ModifiedBy { get; set; }
}