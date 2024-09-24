using ShopTARgv23.Data;
using ShopTARgv23.Core.Domain;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTARgv23.Core.Dto;

namespace ShopTARgv23ApplicationServices.Services
{
    public class SpaceshipsServices : ISpaceshipServices
    {
        private readonly ShopTARgv23Context _context;

        public SpaceshipsServices
            (
               ShopTARgv23Context context
            )
        {
            _context = context;
        }

        public async Task<Spaceship> DetailsAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync( x => x.Id == id);

            return result;

        }

        public async Task<Spaceship> Update (SpaceshipDto dto)
        {
            Spaceship domain = new();

            domain.Id = dto.Id;
            domain.Name = dto.Name;
            domain.Type = dto.Type;
            domain.BuildDate = dto.BuildDate;
            domain.CargoWeight = dto.CargoWeight;
            domain.Crew = dto.Crew;
            domain.EnginePower = dto.EnginePower;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = dto.ModifiedAt;

            _context.Spaceships .Update( domain );
            await _context.SaveChangesAsync();

            return domain;
        }





    }
}
