namespace NetFlow.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Services.Interfaces;
    using NetFlow.Web.ViewModels;

    public class CourseService : BaseService, ICourseService
    {
        public CourseService(NetFlowDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
           : base(context, mapper, userManager, signInManager)
        {
        }

        public Course Create(CreateCourseViewModel model)
        {
            var course = this.Mapper.Map<Course>(model);
            this.context.Courses.Add(course);
            this.context.SaveChanges();

            return course;
        }
    }
}
