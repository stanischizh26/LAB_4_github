using LAB_4.Data;
using LAB_4.Middleware;
using LAB_4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace LAB_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IServiceCollection services = builder.Services;
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            //string connectionString = "Server=DESKTOP-K6GHP6T\\SQLEXPRESS01;Database=SimilarProductsA;Trusted_Connection=True;";

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Добавление контекста базы данных
            builder.Services.AddDbContext<SimilarProductsContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDistributedMemoryCache(); // Для использования кэша
            builder.Services.AddSession(); // Для использования сессий
            builder.Services.AddControllers();

            var app = builder.Build();



            // Включение сессий
            app.UseSession();

            // Подключение middleware для инициализации базы данных
            app.UseDbInitializer();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


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

            app.Run();

        }
    }
}
