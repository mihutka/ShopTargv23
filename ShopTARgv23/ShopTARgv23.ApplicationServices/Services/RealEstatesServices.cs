using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreRealEstate = ShopTARgv23.Core.Domain.RealEstate;
using MigrationRealEstate = ShopTARgv23.Data.Migrations.RealEstate;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class RealEstatesServices : IRealEstateServices
    {
        private readonly ShopTARgv23Context _context;
        private readonly IFileServices _fileServices;
        public RealEstatesServices
            (
                ShopTARgv23Context context
            , IFileServices fileServices
                
            )
        {
            _context = context;
            _fileServices = fileServices;
            
        }

        public async Task<CoreRealEstate> DetailsAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<CoreRealEstate> Create(RealEstateDto dto)
        {
            CoreRealEstate realEstate = new();

            realEstate.Id = Guid.NewGuid();
            realEstate.Location = dto.Location;
            realEstate.Size = dto.Size;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.UpdatedAt = DateTime.Now;
            

            

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, realEstate);
            }
            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }

        public async Task<CoreRealEstate> Update(RealEstateDto dto)
        {
            CoreRealEstate domain = new();

            domain.Id = dto.Id;
            domain.Location = dto.Location;
            domain.Size = dto.Size;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.UpdatedAt = DateTime.Now;
            

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<CoreRealEstate> Delete(Guid id)
        {
            var realestate = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            

           
            _context.RealEstates.Remove(realestate);
            await _context.SaveChangesAsync();

            return realestate;
        }

    }
}
