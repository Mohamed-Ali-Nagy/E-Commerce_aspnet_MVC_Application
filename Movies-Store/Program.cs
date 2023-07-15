using Microsoft.EntityFrameworkCore;
using Movies_Store.Data;

namespace Movies_Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //        builder.Services.AddDbContext<HRContext>(options =>
            //     options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            //);
            builder.Services.AddDbContext<CinemaContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnectionString")));
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            AppDbInitializer.seed(app);


            app.Run();
        }
    }
}