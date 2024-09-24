using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.Spaceships;

namespace ShopTARgv23.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARgv23Context _context;
        private readonly ISpaceshipServices _spaceshipServices;

        

        public SpaceshipsController
            (
            ShopTARgv23Context context,
            ISpaceshipServices spaceshipServices
            )
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
        }


        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    BuildDate = x.BuildDate,
                    EnginePower = x.EnginePower,
                    

                });


            return View(result);
        }

        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailsAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDetailsViewModel();
            vm.Name = spaceship.Name;

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.BuildDate = spaceship.BuildDate;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.EnginePower = spaceship.EnginePower;
            vm.Crew = spaceship.Crew;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            
            return View(vm);

        }

        [HttpGet]

        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailsAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }



            var vm = new SpaceshipCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type= spaceship.Type;
            vm.BuildDate= spaceship.BuildDate;
            vm.CargoWeight= spaceship.CargoWeight;
            vm.Crew= spaceship.Crew;
            vm.EnginePower= spaceship.EnginePower;
            

            return View("CreateUpdate", vm);


        }

        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto();


            vm.Id = dto.Id;
            vm.Name = dto.Name;
            vm.Type = dto.Type;
            vm.BuildDate = dto.BuildDate;
            vm.CargoWeight = dto.CargoWeight;
            vm.EnginePower = dto.EnginePower;
            vm.Crew = dto.Crew;
            vm.CreatedAt = dto.CreatedAt;
            vm.ModifiedAt = dto.ModifiedAt;

            var result = await _spaceshipServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete (Guid id)
        {
            var spaceship = await _spaceshipServices.DetailsAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel();
            
                vm.Id = spaceship.Id;
                vm.Name = spaceship.Name;
                vm.Type = spaceship.Type;
                vm.BuildDate = spaceship.BuildDate;
                vm.CargoWeight = spaceship.CargoWeight;
                vm.EnginePower = spaceship.EnginePower;
                vm.Crew = spaceship.Crew;
                vm.CreatedAt = spaceship.CreatedAt;
                vm.ModifiedAt = spaceship.ModifiedAt;

            

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceship = await _spaceshipServices.Delete(id);

            if (spaceship == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
