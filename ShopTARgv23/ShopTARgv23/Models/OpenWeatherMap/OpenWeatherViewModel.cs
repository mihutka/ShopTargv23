﻿using ShopTARgv23.Core.Dto.WeatherDtos;

namespace ShopTARgv23.Models.OpenWeatherMap
{
    public class OpenWeatherViewModel
    {
        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public long Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public long Dt { get; set; }
        public Sys Sys { get; set; }
        public long Timezone { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public long Cod { get; set; }
        public long All { get; set; }

        public double Lon { get; set; }
        public double Lat { get; set; }

        public double Temperature { get; set; }
        public double feels_like { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public long Pressure { get; set; }
        public long Humidity { get; set; }
        public long SeaLevel { get; set; }
        public long GrndLevel { get; set; }

        public long Type { get; set; }
        public long IdSys { get; set; }
        public string Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }

        public long IdWeather { get; set; }
        public string MainWeather { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public double Speed { get; set; }
        public long Deg { get; set; }
        public double WindSpeed { get; internal set; }
        public string WeatherIcon { get; internal set; }
    }
}
