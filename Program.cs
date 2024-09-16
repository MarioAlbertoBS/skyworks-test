using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyworksTest.Helpers;
using SkyworksTest.Helpers.Filters;
using SkyworksTest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add db context
builder.Services.AddDbContext<SkyworksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressInferBindingSourcesForParameters = true;
});

var app = builder.Build();

if (args.Contains("seed")) {
    using (var scope = app.Services.CreateScope())  {
        var services = scope.ServiceProvider;

        try {
            var context = services.GetRequiredService<SkyworksDbContext>();

            // Execute Seeder
            Seeder.Seed(context);
            Console.WriteLine("Seeder Executed Successfully.");
        } catch (Exception e) {
            Console.WriteLine($"Error Executing Seeder: {e.Message}");;
        }
    }

    return;
}

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