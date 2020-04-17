using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        [ViewData]
        public string Message { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
            var carApiUrl = Environment.GetEnvironmentVariable("MyApiUrl");

            var responseData = string.Empty;
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri(carApiUrl + "api/WeatherForecast/GetWeather/3"); // ASP.NET 3 (VS 2019 only)                
                var response = await client.SendAsync(request);
                responseData = await response.Content.ReadAsStringAsync();
            }

            Message = responseData;
        }
    }
}
