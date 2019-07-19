namespace NetFlow.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using NetFlow.Data;
    using NetFlow.Data.Models;

    public abstract class BaseService
    {
        protected BaseService(NetFlowDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.Mapper = mapper;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public NetFlowDbContext context { get; set; }

        public IMapper Mapper { get; set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }
    }
}
