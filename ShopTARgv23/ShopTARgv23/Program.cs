using ShopTARgv23.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.ApplicationServices.Services;
using Microsoft.Extensions.FileProviders;

namespace ShopTARgv23
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ISpaceshipServices, SpaceshipsServices>();
            builder.Services.AddScoped<IKindergartenServices, KindergartenServices>();
            builder.Services.AddScoped<IFileServices, FileServices>();
            builder.Services.AddScoped<IOpenWeatherServices, OpenWeatherServices>();

            builder.Services.AddDbContext<ShopTARgv23Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("ShopTARgv23.Data")));  


            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider
                (Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
                RequestPath = "/multipleFileUpload"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
