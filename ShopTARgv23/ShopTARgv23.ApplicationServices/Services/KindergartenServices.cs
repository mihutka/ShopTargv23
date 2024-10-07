using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using System;
using System.Threading.Tasks;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARgv23Context _context;

        public KindergartenServices(ShopTARgv23Context context)
        {
            _context = context;
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

            _context.Kindergartens.Update(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

       
        public async Task<Kindergarten> Delete(Guid id)
        {
            var kindergarten = await _context.Kindergartens.FirstOrDefaultAsync(x => x.Id == id);

            if (kindergarten != null)
            {
                _context.Kindergartens.Remove(kindergarten);
                await _context.SaveChangesAsync();
            }

            return kindergarten;
        }
    }
}
