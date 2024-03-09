using Beershop24.Data;
using Beershop24.Domains.Data;
using Beershop24.Domains.Entities;
using Beershop24.Repositories;
using Beershop24.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<BeerDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

// Dependency injections

builder.Services.AddTransient<IService<Beer>, BeerService>();
builder.Services.AddTransient<IService<Brewery>, BreweryService>();
builder.Services.AddTransient<IService<Variety>, VarietyService>();

builder.Services.AddTransient<IDAO<Beer>, BeerDAO>();
builder.Services.AddTransient<IDAO<Brewery>, BreweryDAO>();
builder.Services.AddTransient<IDAO<Variety>, VarietyDAO>();

// Add Automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.VIVES.Session";
    // minimum one second
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Beer}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
