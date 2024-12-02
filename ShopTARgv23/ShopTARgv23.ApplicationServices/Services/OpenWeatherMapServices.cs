using Nancy.Json;
using ShopTARgv23.Core.Dto.WeatherDtos;
using ShopTARgv23.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ShopTARgv23.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string openApiKey = "9e82a8e542b3603dcfb99d9b0da2a2a0";
            string url = $"https://api.openweathermap.org/data/2.5/weather?units=metric&q={dto.Name}&appid={openApiKey}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                Console.WriteLine(json);  
                OpenWeatherRootDto openweatherRootDto = new JavaScriptSerializer()
                    .Deserialize<OpenWeatherRootDto>(json);

                dto.Coord = openweatherRootDto.Coord;
                dto.Weather = openweatherRootDto.Weather;
                dto.Base = openweatherRootDto.Base;
                dto.Main = openweatherRootDto.Main;
                dto.Visibility = openweatherRootDto.Visibility;
                dto.Wind = openweatherRootDto.Wind;
                dto.Clouds = openweatherRootDto.Clouds;
                dto.Dt = openweatherRootDto.Dt;
                dto.Sys = openweatherRootDto.Sys;
                dto.Timezone = openweatherRootDto.Timezone;
                dto.Id = openweatherRootDto.Id;
                dto.Name = openweatherRootDto.Name;
                dto.Cod = openweatherRootDto.Cod;

                dto.Clouds = openweatherRootDto.Clouds;
                dto.Clouds.All = openweatherRootDto.Clouds.All;

                dto.Coord.Lat = openweatherRootDto.Coord.Lat;
                dto.Coord.Lon = openweatherRootDto.Coord.Lon;

                dto.Main.Temp = openweatherRootDto.Main.Temp;
                dto.Main.feels_like = openweatherRootDto.Main.feels_like;
                dto.Main.TempMin = openweatherRootDto.Main.TempMin;
                dto.Main.TempMax = openweatherRootDto.Main.TempMax;
                dto.Main.Pressure = openweatherRootDto.Main.Pressure;
                dto.Main.Humidity = openweatherRootDto.Main.Humidity;
                dto.Main.SeaLevel = openweatherRootDto.Main.GrndLevel;

                dto.Sys.Type = openweatherRootDto.Sys.Type;
                dto.Sys.Id = openweatherRootDto.Sys.Id;
                dto.Sys.Country = openweatherRootDto.Sys.Country;
                dto.Sys.Sunrise = openweatherRootDto.Sys.Sunrise;
                dto.Sys.Sunset = openweatherRootDto.Sys.Sunset;

                dto.Wind.Speed = openweatherRootDto.Wind.Speed;
                dto.Wind.Deg = openweatherRootDto.Wind.Deg;



                return dto;


            }
        }
    }
}
