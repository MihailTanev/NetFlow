namespace NetFlow.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Data.Models;
    using NetFlow.Data;
    using NetFlow.Test.Common;
    using Xunit;
    using NetFlow.Services.Courses;
    using NetFlow.Services.Courses.Models;

    public class CourseServiceTests
    {
        private ICourseService courseService;

        private List<Course> GetDummyData()
        {
            return new List<Course>()
            {
                new Course
                {
                    Name = "React",
                    Credit = 15,
                    StartDate = DateTime.UtcNow.AddDays(-15),
                    EndDate = DateTime.UtcNow.AddDays(5),
                    Description = "some info",
                    Picture = "src/pics/testimage/testimage",
                    TeacherId = "0575dea3-336a-46e9-8452-6880b959f188"
                },
                new Course
                {
                    Name = "Angular",
                    Credit = 10,
                    StartDate = DateTime.UtcNow.AddDays(-15),
                    EndDate = DateTime.UtcNow.AddDays(5),
                    Description = "some info",
                    Picture = "src/pics/testimage/testimage",
                    TeacherId = "0575dea3-336a-46e9-8452-6880b959f188"
                }
            };
        }

        private async Task SeedData(NetFlowDbContext context)
        {
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        public CourseServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldSuccessfullyCreate()
        {
            //string errorMessagePrefix = "ProductService Create() method does not work properly.";

            var context = StopifyDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.courseService = new CourseService(context);

            CourseServiceModel testProduct = new CourseServiceModel
            {
                Name = "Pesho",
                Credit = 5,
                StartDate = DateTime.UtcNow,
                Picture = "src/res/default.png",
                
            };

            //var actualResult = await this.courseService.CreateCourse(testProduct);
            //Assert.True(actualResult, errorMessagePrefix);
        }

    }
}
