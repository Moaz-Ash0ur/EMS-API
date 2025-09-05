using EMS.BLL.Interface;
using EMS.BLL.ProfileMapping;
using EMS.BLL.Services;
using EMS.DAL.Contracts;
using EMS.DAL.Data;
using EMS.DAL.Models;
using EMS.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace EMS_API.Helper
{

    public static class RegisterServiceHelper
    {

        public static void RegisterService(this WebApplicationBuilder builder)
        {


            builder.Services.AddSwaggerGenJwtAuth();

            //Configration EMS Project
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));


            //Log error
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                tableName: "Log",
               autoCreateSqlTable: true)
                .CreateLogger();

            builder.Host.UseSerilog();



            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAllowanceService, AllowanceService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<ILeaveService, LeaveService>();
            builder.Services.AddScoped<IAppreciationService, AppreciationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IRefreshTokens, RefreshTokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();



            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // JWT
            builder.Services.CustomJwtAuthConfig(builder.Configuration);

      


        }

    }

}
