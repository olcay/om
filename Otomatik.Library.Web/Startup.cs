using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Otomatik.Library.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otomatik.Library.Web.Areas.Identity.Data;
using Otomatik.Library.Web.Core;
using Otomatik.Library.Web.Core.Models;
using Otomatik.Library.Web.Core.Repositories;
using Otomatik.Library.Web.Data.Repositories;
using Otomatik.Library.Web.Extensions;
using Otomatik.Library.Web.Services;
using Otomatik.Library.Web.Services.ApiClients;

namespace Otomatik.Library.Web
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

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.MaxLengthForKeys = 128;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                AdditionalUserClaimsPrincipalFactory>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            var builder = services.AddRazorPages();

#if DEBUG
            builder.AddRazorRuntimeCompilation();
#endif

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddTransient<IBookFinder, BookFinder>();
            services.AddHttpClient<RawgGamesClient>();

            //Persistence
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IFollowingRepository, FollowingRepository>();
            services.AddTransient<IShelfRepository, ShelfRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IStarRepository, StarRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookDetailRepository, BookDetailRepository>();
            services.AddTransient<IBookAuthorRepository, BookAuthorRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IItemBookDetailRepository, ItemBookDetailRepository>();

            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
