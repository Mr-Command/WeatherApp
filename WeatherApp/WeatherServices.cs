

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp;

public class WeatherService
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public WeatherService(string apiKey)
    {
        _apiKey = apiKey;
        _client = new HttpClient();
    }

    public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon)
    {
        try
        {
            var response = await _client.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&lang=tr");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherResponse>(content);
        }
        catch (Exception ex)
        {
            throw new Exception("Hava durumu verileri alınamadı", ex);
        }
    }
}
