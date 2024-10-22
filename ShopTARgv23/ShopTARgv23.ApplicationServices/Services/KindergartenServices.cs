using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARgv23Context _context;
        private readonly IFileServices _fileServices;


        public KindergartenServices(ShopTARgv23Context context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }


        public async Task<Kindergarten> DetailsAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new Kindergarten
            {
                Id = Guid.NewGuid(),
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                Teacher = dto.Teacher,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, kindergarten);
            }
            await _context.Kindergartens.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

       
        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            var kindergarten = await _context.Kindergartens.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (kindergarten == null)
            {
                return null;
            }

            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.Teacher = dto.Teacher;
            kindergarten.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, kindergarten);
            }

            _context.Kindergartens.Update(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

       
        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            var photos = await _context.FileToDatabases
                 .Where(x => x.KindergartenId == id)
                 .Select(y => new FileToDataBaseDto
                 {
                     Id = y.Id,
                     ImageTitle = y.ImageTitle,
                     KindergartenId = y.KindergartenId

                 }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(photos);
            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
