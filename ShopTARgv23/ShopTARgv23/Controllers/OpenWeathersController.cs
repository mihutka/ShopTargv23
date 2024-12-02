using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto.WeatherDtos;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.OpenWeatherMap;

namespace ShopTARgv23.Controllers
{

    public class OpenWeathersController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeathersController(IOpenWeatherServices openWeatherServices)
        {
            _openWeatherServices = openWeatherServices;
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SearchCity(OpenWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("City", "OpenWeathers", new { city = model.Name });
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> City(string city)
        {
            OpenWeatherResultDto dto = new OpenWeatherResultDto
            {
                Name = city
            };


            await _openWeatherServices.OpenWeatherResult(dto);

            OpenWeatherViewModel vm = new OpenWeatherViewModel
            {
                Name = dto.Name,
                Temperature = dto.Main?.Temp ?? 0,
                feels_like = dto.Main?.feels_like ?? 0,
                Humidity = dto.Main?.Humidity ?? 0,
                Pressure = dto.Main?.Pressure ?? 0,
                TempMin = dto.Main?.TempMin ?? 0,
                TempMax = dto.Main?.TempMax ?? 0,
                Description = dto.Weather?.FirstOrDefault()?.Description ?? "No description available",
                WindSpeed = dto.Wind?.Speed ?? 0,
                Deg = dto.Wind?.Deg ?? 0,
                SeaLevel = dto.Main?.SeaLevel ?? 0,
                GrndLevel = dto.Main?.GrndLevel ?? 0,
                Clouds = dto.Clouds,
                All = dto.Clouds?.All ?? 0,
                Timezone = dto.Timezone,
                Lon = dto.Coord?.Lon ?? 0,
                Lat = dto.Coord?.Lat ?? 0,
                Type = dto.Sys?.Type ?? 0,
                IdSys = dto.Sys?.Id ?? 0,
                Sunrise = dto.Sys?.Sunrise ?? 0,
                Sunset = dto.Sys?.Sunset ?? 0,




            };


            return View(vm);
        }

    }
}
