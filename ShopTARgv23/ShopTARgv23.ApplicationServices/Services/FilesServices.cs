
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class FileServices 
    {
        private readonly IHostEnvironment _webHost;

        private readonly ShopTARgv23Context  _context;

        public FileServices
            (
                 ShopTARgv23Context context,
                 IHostEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }

       
        public async void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }
            }

            foreach (var image in dto.Files)
            {
                string uploadFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream =  new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);

                    FileToApi path = new FileToApi()
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        SpaceshipId = spaceship.Id,

                    };
                    
                    _context.FileToApis.AddAsync(path);


                }
            }
        }


    }
}
