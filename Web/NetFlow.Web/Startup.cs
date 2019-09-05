namespace NetFlow.Web
{
    using NetFlow.Services.Administrator.Models;
    using System.Reflection;
    using CloudinaryDotNet;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NetFlow.Data.Models;
    using NetFlow.Web.Middleware.Extensions;
    using NetFlow.Services.Mapping;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Web.Extensions.ServiceCollections;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });         


            // Configure Db context
            services.AddDbContext<NetFlowDbContext>(options =>
                           options.UseSqlServer(
                               Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<NetFlowDbContext>()
                .AddDefaultTokenProviders();

            // Configure Urls to lowercase
            services.AddRouting(routing => routing.LowercaseUrls = true);

            //Configure Cloudinary
            Account cloudinaryCredential = new Account(
               this.Configuration["Cloudinary:CloudName"],
               this.Configuration["Cloudinary:ApiKey"],
               this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryCredential);

            services.AddSingleton(cloudinary);


            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 5;
            });

            // Add Service extension
            services.AddApplicationServices();

            // Configure global Antiforgery
            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                 typeof(AdministratorServiceModel).GetTypeInfo().Assembly,
                 typeof(CourseServiceModel).GetTypeInfo().Assembly,
                 typeof(CreateCourseViewModel).GetTypeInfo().Assembly);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Middleware for adding roles in the database
            app.UseSeedRoles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "profile",
                    template: "profile/users/{userId}",
                    defaults: new { area = AreaConstants.PROFILE_AREA, controller = "Users", action = "Index" });

                routes.MapRoute(
                  name: "trainings",
                  template: "trainings/courses/{id}/{name}",
                  defaults: new { area = AreaConstants.TRAININGS_AREA, controller = "Courses", action = "Details" });

                routes.MapRoute(
                    name: "blog",
                    template: "blog/posts/{id}/{title}",
                    defaults: new { area = AreaConstants.BLOG_AREA, controller = "Posts", action = "Details" });

                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");               
            });
        }
    }
}
