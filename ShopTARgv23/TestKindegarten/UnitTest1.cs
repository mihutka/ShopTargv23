using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.TestKindergarten;
using Xunit;

namespace TestKindegarten
{
    public class UnitTest1 : TestBase
    {

        [Fact]

        public async Task Should_Create_KindergartenTeacher()
        {
            var dto = new KindergartenDto
            {
                Teacher = "wasd",
                KindergartenName = "Test",
                GroupName = "Test",
            };

            var createKinderGartenTeacher = await Svc<IKindergartenServices>().Create(dto);

            Assert.Equal(dto.Teacher, createKinderGartenTeacher.Teacher);
        }

        [Fact]

        public async Task Should_Update_KindergartenChildrenCount()
        {
            var dto = new KindergartenDto
            {
                Teacher = "wasd",
                KindergartenName = "Test",
                GroupName = "Test",
                ChildrenCount = 15,
            };

            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            var updatedDto = new KindergartenDto
            {
                Id = createdKindergarten.Id,
                Teacher = "wasd",
                KindergartenName = "Test",
                GroupName = "Test",
                ChildrenCount = 10,
            };


             createdKindergarten = await Svc<IKindergartenServices>().Update(updatedDto);

            Assert.NotEqual(dto.ChildrenCount, createdKindergarten.ChildrenCount);
        }



        [Fact]
        public async Task Should_ReturnNull_WhenKindergartenNotFound()
        {
            Guid nonExistingId = Guid.NewGuid();
            var result = await Svc<IKindergartenServices>().DetailsAsync(nonExistingId);
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Update_KindergartenGroupName()
        {
            
            var dto = new KindergartenDto
            {
                Teacher = "wasd",
                KindergartenName = "wasd",
                GroupName = "asdsad",
                ChildrenCount = 25,
            };

            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            var updatedDto = new KindergartenDto
            {
                Id = createdKindergarten.Id,
                Teacher = "wasd",
                KindergartenName = "wasd",
                GroupName = "asdas", 
                ChildrenCount = 25,
            };

            
            var updatedKindergarten = await Svc<IKindergartenServices>().Update(updatedDto);

            
            Assert.NotNull(updatedKindergarten);
            Assert.Equal(updatedDto.GroupName, updatedKindergarten.GroupName);
            Assert.NotEqual(dto.GroupName, updatedKindergarten.GroupName);
        }








    }
}