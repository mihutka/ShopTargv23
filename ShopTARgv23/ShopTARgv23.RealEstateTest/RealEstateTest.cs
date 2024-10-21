
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data.Migrations;
using System;
using System.Drawing;
using System.Xml;


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


        [Fact]

        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealestate()
        {
            RealEstateDto realEstate = MockRealEstateData();

             var create  = await Svc<IRealEstateServices>().Create(realEstate);

            var result = await Svc<IRealEstateServices>().Delete((Guid)create.Id);
           
            Assert.Equal(create.Id, result.Id);
            
        }

        [Fact]

        public async Task ShouldNot_DeleteByIdRealeEstate_WhenDidNotDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var realEstate1 = await Svc<IRealEstateServices>().Create(realEstate);
            var realEstate2 = await Svc<IRealEstateServices>().Create(realEstate);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);
            

           Assert.NotEqual(realEstate1.Id, result.Id);
            

        }

        [Fact]

        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {

            var guid = Guid.Parse("dad9f4b9-e301-499b-8c3e-40bf95b8d950");

            //uued andmed
            RealEstateDto realEstate = MockRealEstateData();

            //andmebaasis olevad andmed
            RealEstateDto domain = new();

            domain.Id = Guid.Parse("dad9f4b9-e301-499b-8c3e-40bf95b8d950");
            domain.Location = "qwerty";
            domain.Size = 34;
            domain.RoomNumber = 1234;
            domain.BuildingType = "qwerty";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

           await Svc<IRealEstateServices>().Update(realEstate);



            Assert.Equal(domain.Id, guid);
            Assert.DoesNotMatch(domain.Location, realEstate.Location);
            Assert.DoesNotMatch(domain.RoomNumber.ToString(), realEstate.RoomNumber.ToString());
            Assert.NotEqual(domain.Size, realEstate.Size);











        }

        [Fact]

        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            RealEstateDto dto = MockRealEstateData();

            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto update = MockRealEstateData2();

            var result = await Svc<IRealEstateServices>().Update(update);

            Assert.NotEqual(update.Size, result.Size);
            Assert.DoesNotMatch(update.Location, result.Location);
            Assert.NotEqual(update.ModifiedAt, result.ModifiedAt);






        }




        private RealEstateDto  MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                
                 Location = "asd",
                 Size = 100,
                 RoomNumber = 1,
                 BuildingType = "asd",
                 CreatedAt = DateTime.Now,
                 ModifiedAt = DateTime.Now
            };

            return realEstate;
        }

        private RealEstateDto MockRealEstateData2()
        {
            RealEstateDto realEstate = new()
            {
                Location = "dfsdfd",
                Size = 110,
                RoomNumber = 2,
                BuildingType = "sdfsdfds",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            return realEstate;
        }



        
        
        





    }
}