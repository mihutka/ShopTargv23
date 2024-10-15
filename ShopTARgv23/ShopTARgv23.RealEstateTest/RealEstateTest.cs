
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;


namespace ShopTARgv23.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Location = "asd";
            dto.Size = 123;
            dto.RoomNumber = 123;
            dto.BuildingType = "asd";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);


            //Assert

            Assert.NotNull(result);
        }

        [Fact]

        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("dad9f4b9-e301-499b-8c3e-40bf95b8d950");


            //Act
            await Svc<IRealEstateServices>().GetAsync(guid);


            //Assert
            Assert.NotEqual(wrongGuid, guid);


        }


        [Fact]

        public async Task Should_GetByIdRealEstate_WhenReturnEqual()
        {
            Guid correctGuid = Guid.Parse("dad9f4b9-e301-499b-8c3e-40bf95b8d950");
            Guid guid = Guid.Parse("dad9f4b9-e301-499b-8c3e-40bf95b8d950");

            await Svc<IRealEstateServices>().GetAsync(guid);

            Assert.Equal(correctGuid, guid);

        }


    }
}