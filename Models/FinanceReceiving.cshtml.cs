using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hariankompas.Pages
{
    public class FinanceReceivingModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public FinanceReceivingModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:8000/api/FinanceReceiving/Read");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<FinanceData>(jsonString);

                ViewData["Rows"] = data.Rows;
            }
            else
            {
                // Handle if the request failed
            }
        }
    }

    public class FinanceData
    {
        public List<FinanceRow>? Rows { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }
        public string? MessageDescription { get; set; }
        public int RowCount { get; set; }
        public List<string>? Columns { get; set; }
    }

    public class FinanceRow
    {
        public int ID { get; set; }
        public string? Number { get; set; }
        public DateTime ReceivingDate { get; set; }
        public string? WarehouseName { get; set; }
        public string? CompanyName { get; set; }
        public string? ReferenceGRNumber { get; set; }
        public string? SupplierReference { get; set; }
        public string? SupplierName { get; set; }
    }
}