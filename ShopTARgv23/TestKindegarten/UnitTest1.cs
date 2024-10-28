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


        public async Task Should_Delete_Kindergarten()
        {
            

            var dto = new KindergartenDto
            {
                
                Teacher = "wasd",
                KindergartenName = "Test",
                GroupName = "Test",
                ChildrenCount = 15,
                
            };

            var dto2 = new KindergartenDto
            {
                Teacher = "wasd",
                KindergartenName = "Test",
                GroupName = "Test",
                ChildrenCount = 15,
            };

            Assert.Equal(dto.GroupName, dto2.GroupName);



            var createDto = await Svc<IKindergartenServices>().Create(dto);

            var createDto2 = await Svc<IKindergartenServices>().Create(dto2);

            var deleteKindergartenGroup = await Svc<IKindergartenServices>().Delete((Guid)createDto.Id);

            
            Assert.Null(deleteKindergartenGroup);
            Assert.NotNull(createDto2);

        }


    }
}