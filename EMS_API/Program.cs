using EMS.BLL.Interface;
using EMS.BLL.Interface.Base;
using EMS.BLL.ProfileMapping;
using EMS.BLL.Services;
using EMS.DAL.Contracts;
using EMS.DAL.Data;
using EMS.DAL.Models;
using EMS.DAL.Repositories;
using EMS_API.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.RegisterService();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // ضع رابط الواجهة (مثلاً لو تشغلها من VS Code Live Server)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // مهم عشان الكوكي RefreshToken
    });
}); 

//-------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Apply migrations
    await dbContext.Database.MigrateAsync();

    // Seed data
    await ContextConfig.SeedDataAsync(dbContext, userManager, roleManager);
}




app.Run();
