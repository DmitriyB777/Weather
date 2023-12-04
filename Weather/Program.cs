using Microsoft.EntityFrameworkCore;
using Weather.Context;
using Weather.Interfaces.Repositories;
using Weather.Interfaces.Services;
using Weather.Repositories;
using Weather.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WeatherContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IUnarchivingService, UnarchivingService>();
builder.Services.AddTransient<IParserService, ParserService>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

builder.Services.AddScoped<IUserErrorService, UserErrorService>();
builder.Services.AddScoped<WeatherContext>();

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

app.Run();
