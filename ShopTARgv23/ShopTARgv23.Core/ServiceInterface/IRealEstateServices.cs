using ShopTARgv23.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> DetailsAsync(Guid id);
    }
}
