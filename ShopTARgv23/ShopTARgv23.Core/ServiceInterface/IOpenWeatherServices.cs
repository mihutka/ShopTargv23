using ShopTARgv23.Core.Dto.WeatherDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IOpenWeatherServices
    {
        Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto);
    }
}
