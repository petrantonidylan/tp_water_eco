using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using System.Windows;
using Google.Protobuf.WellKnownTypes;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController
    {
        [HttpGet(Name = "GetWeather")]
        public WeatherResponse GetWeather()
        {
            string apiUriPart1 = "https://api.meteo-concept.com/api/forecast/daily";
            string apiUriPart2 = "?token=ec041527f2d9577181a87caf3b40edf37cca156ff7c253b1290a65cc809d3091&insee=11262";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUriPart1 + apiUriPart2).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    WeatherResponse weatherData = JsonSerializer.Deserialize<WeatherResponse>(responseBody);
                    //List<WeatherResponse> weatherList = JsonSerializer.Deserialize<List<WeatherResponse>>(responseBody);

                    return weatherData;
                }else
                {
                    return null;
                }
            }
        }

    }
}
