using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.Data;
using WorkingWithEFCore.DataContext;

var builder = WebApplication.CreateBuilder(args);
var baseAddress = "";
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("webUrl", x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebUrl"));
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
baseAddress = builder.Configuration.GetValue<string>("WebUrl");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
