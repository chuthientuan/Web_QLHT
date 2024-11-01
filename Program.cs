using BTL.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Đăng ký DbContext
            builder.Services.AddDbContext<QlhieuThuocContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BTLContext")));

            // Thêm dịch vụ MVC
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Home/Index"; // Đường dẫn tới trang đăng nhập
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Đường dẫn tới trang từ chối truy cập
                });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            // Cấu hình middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
