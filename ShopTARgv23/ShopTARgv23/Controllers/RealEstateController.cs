using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.ApplicationServices.Services;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.Realestate;
using ShopTARgv23.Models.Spaceships;

namespace ShopTARgv23.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopTARgv23Context _context;
        private readonly IRealEstateServices _realestateServices;

        public RealEstateController
            (
                ShopTARgv23Context context,
                IRealEstateServices realestateServices
                
            )
        {
            _context = context;
            _realestateServices = realestateServices;
        }

        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Location = x.Location,
                    Size = x.Size,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType
                });

            return View(result);
        }


    }

}
