using FluentValidation;
using Lesson4.Data;
using Lesson4.Models;
using Lesson4.Services;
using Lesson4.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("PetsDb")
    ?? throw new InvalidOperationException("Не найдена строка подключения PetsDb");

builder.Services.AddDbContext<CatDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.AddTransient<CatService>();
builder.Services.AddTransient<IRepository, DbRepository>();
builder.Services.AddScoped<IValidator<Cat>, CatEditValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
