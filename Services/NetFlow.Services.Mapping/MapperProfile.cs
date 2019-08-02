using AutoMapper;
using NetFlow.Data.Models;
using NetFlow.Web.ViewModels.User;

namespace NetFlow.Services.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //courses
            //this.CreateMap<Task, AddCourseViewModel>()
            //    .ForMember(evm => evm.TeacherId, e => e.MapFrom(x => x.TeacherId))                
            //    .ReverseMap();

            this.CreateMap<User, UsersViewModel>()
                .ForMember(uvm => uvm.Id, u => u.MapFrom(x => x.Id))
                .ForMember(uvm => uvm.Username, u => u.MapFrom(x => x.UserName))
                .ReverseMap();
        }
    }
}
