using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class ApiService
    {
        public const string BaseUrl = "https://api.openweathermap.org/data/2.5/";
        public const string AppId = "a8e17e406cdbc8dad3a13d1f29d1fc6e";

        public async Task<Root> GetWeather(double latitude, double longitude)
        {
            try
            {
                var httpClient = new HttpClient();
                var uri = $"{BaseUrl}forecast?lat={latitude}&lon={longitude}&appid={AppId}";
                var response = await httpClient.GetAsync(uri);
                var result = await ProcessResponseAsync(response);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static async Task<Root> GetWeatherByCity(string city)
        {
            try
            {
                var httpClient = new HttpClient();
                var uri = $"{BaseUrl}weather?q={city}&appid={AppId}";
                var response = await httpClient.GetAsync(uri);
                var result = await ProcessResponseAsync(response);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        //https://api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}



        public static async Task<Root> ProcessResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new Exception("No Data today");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<Root>(responseContent, options) ?? new Root();
        }

    }
}
