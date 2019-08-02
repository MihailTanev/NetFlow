﻿using Microsoft.AspNetCore.Builder;
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
using NetFlow.Services.Users;
using NetFlow.Services.Users.Interface;
using Stopify.Services.Mapping;
using NetFlow.Services.Users.Models;
using System.Reflection;
using NetFlow.Services.Courses;
using NetFlow.Services.Courses.Interface;
using AutoMapper;

namespace NetFlow.Web
{
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
                options.CheckConsentNeeded = context => true;
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

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICourseService, CourseService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                 typeof(UserServiceModel).GetTypeInfo().Assembly);


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
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
