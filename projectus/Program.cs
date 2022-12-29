using Microsoft.AspNetCore.Authentication.Cookies;
using projectus.BL;
using projectus.BL.Interfaces;
using projectus.DAL;
using projectus.DAL.Interfaces;

namespace projectus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
				options =>
				{
					options.LoginPath = new PathString("/Users/Login");
					options.AccessDeniedPath = new PathString("/Users/Login");
					options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				});
			builder.Services.AddAuthorization();

			builder.Services.AddSingleton<IUserDAL, UserDBRepo>();
			builder.Services.AddSingleton<IUserBL, UserBL>();
			builder.Services.AddSingleton<IGameRecordDAL, GameRecordDBRepo>();
			builder.Services.AddSingleton<IGameRecordBL, GameRecordBL>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}