using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OtomatikMuhendis.Kutuphane.Web.Core;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using OtomatikMuhendis.Kutuphane.Web.Data;
using OtomatikMuhendis.Kutuphane.Web.Data.Repositories;
using OtomatikMuhendis.Kutuphane.Web.Extensions;
using OtomatikMuhendis.Kutuphane.Web.Services;
using OtomatikMuhendis.Kutuphane.Web.Services.ApiClients;
using System;
using System.Net.Http;

namespace OtomatikMuhendis.Kutuphane.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(new Connection(Environment.GetEnvironmentVariable("DATABASE_URL")).String));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IBookFinder, BookFinder>();

            //Persistence
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddTransient<IFollowingRepository, FollowingRepository>();
            services.AddTransient<IShelfRepository, ShelfRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IBookDetailRepository, BookDetailRepository>();
            services.AddTransient<IBookAuthorRepository, BookAuthorRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IItemBookDetailRepository, ItemBookDetailRepository>();

            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            services.AddTransient<IRawgGamesClient>(s => new RawgGamesClient(new HttpClient()));

            services.Configure<WebsiteOptions>(Configuration.GetSection("Website"));
            
            services.AddMvc();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
