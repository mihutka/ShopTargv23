using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class RealEstatesServices : IRealEstateServices
    {
        private readonly ShopTARgv23Context _context;

        public RealEstatesServices
            (
                ShopTARgv23Context context
                
            )
        {
            _context = context;
            
        }

        public async Task<RealEstate> DetailsAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }



    }
}
