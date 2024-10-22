using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using System.Xml;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);

        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);

        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);

        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);

        Task<FileToDataBase> RemoveImageFromDatabase(FileToDataBaseDto dto);

        Task<FileToDataBase> RemoveImagesFromDatabase(FileToDataBaseDto[] dtos);

    }
}
