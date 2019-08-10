namespace NetFlow.Test.Common
{
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.Mapping;
    using System.Reflection;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CourseServiceModel).GetTypeInfo().Assembly,
                typeof(Course).GetTypeInfo().Assembly);
        }
    }
}
