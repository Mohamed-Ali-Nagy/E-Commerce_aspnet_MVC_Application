using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movies_Store.Data;
using Movies_Store.Data.Cart;
using Movies_Store.Data.Services;
using Movies_Store.Models;

namespace Movies_Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
          
            builder.Services.AddDbContext<CinemaContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnectionString")));
            builder.Services.AddScoped<IActorService,ActorService>();
            builder.Services.AddScoped<IProducerService,ProducerServices>();
            builder.Services.AddScoped<ICinemaService,CinemaService>();
            builder.Services.AddScoped<IMovieService,MovieService>();
            builder.Services.AddScoped<IOrderService,OrderService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
            builder.Services.AddSession();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CinemaContext>().AddDefaultTokenProviders();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movie}/{action=Index}/{id?}");

            AppDbInitializer.seed(app);
            AppDbInitializer.seedUserAndRoleAsync(app);


            app.Run();
        }
    }
}