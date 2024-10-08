using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.ApplicationServices.Services;
using ShopTARgv23.Core.Dto;
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

        [HttpGet]

        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel realestate = new();

            return View("CreateUpdate", realestate);
        }

        [HttpPost]

        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Location = vm.Location,
                Size = vm.Size,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
         
            };

            var result = await _realestateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realestate = await _realestateServices.DetailsAsync(id);

            if ( realestate == null)
            {
                return NotFound();
            }

            

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realestate.Id;
            vm.Location = realestate.Location;
            vm.Size = realestate.Size;
            vm.RoomNumber = realestate.RoomNumber;
            vm.BuildingType = realestate.BuildingType;
            

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Location = vm.Location,
                Size = vm.Size,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
              
            };

            var result = await _realestateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realestateServices.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Location = realEstate.Location;
            vm.Size = realEstate.Size;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;
            

            return View(vm);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realestateServices.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

           

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Location = realEstate.Location;
            vm.Size = realEstate.Size;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;
            

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realestateServices.Delete(id);

            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
