using Nancy.Json;
using ShopTARgv23.Core.Dto.WeatherDtos;
using ShopTARgv23.Core.ServiceInterface;
using System.Net;


namespace ShopTARgv23.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {

        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "2LBA498kppCQjyb9ZAh5IgNuMYgZZDEr";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={accuApiKey}&q={dto.CityName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                AccuLocationRootDto accuResult = new JavaScriptSerializer().Deserialize<AccuLocationRootDto>(json);

                dto.CityName = accuResult.LocalizedName;
                dto.CityCode = accuResult.Key;
                dto.Rank = accuResult.Rank;

            }

                return dto;
        }
    }
}
