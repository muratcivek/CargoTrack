using CargoTrack.Business;
using CargoTrack.Business.Services.Abouts;
using CargoTrack.Business.Services.Branches;
using CargoTrack.Business.Services.Cities;
using CargoTrack.DataAccess.Context;
using CargoTrack.DataAccess.Repositories.Abouts;
using CargoTrack.DataAccess.Repositories.Branches;
using CargoTrack.DataAccess.Repositories.Cities;
using CargoTrack.Entity.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//IOC Container
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<BusinessAssembly>();

builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();


builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ICityService, CityService>();



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLazyLoadingProxies();

});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.LogoutPath = "/Login/Logout";
    options.AccessDeniedPath = "/Login/AccessDenied";
    options.Cookie.Name = "CargoTrackAuthCookie";
});

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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

// Uygulama başlarken Seed Data ekleme işlemi
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Veritabanının var olduğundan emin olun (Migration kullanıyorsanız context.Database.Migrate() yapın)
    context.Database.EnsureCreated();

    // Eğer veritabanında hiç şehir yoksa ekle
    if (!context.Cities.Any())
    {
        var cities = new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "İstanbul" },
            new City { Id = Guid.NewGuid(), Name = "Ankara" },
            new City { Id = Guid.NewGuid(), Name = "İzmir" },
            new City { Id = Guid.NewGuid(), Name = "Bursa" },
            new City { Id = Guid.NewGuid(), Name = "Antalya" },
            new City { Id = Guid.NewGuid(), Name = "Adana" },
            new City { Id = Guid.NewGuid(), Name = "Konya" },
            new City { Id = Guid.NewGuid(), Name = "Şanlıurfa" },
            new City { Id = Guid.NewGuid(), Name = "Gaziantep" },
            new City { Id = Guid.NewGuid(), Name = "Kocaeli" },
            new City { Id = Guid.NewGuid(), Name = "Mersin" },
            new City { Id = Guid.NewGuid(), Name = "Diyarbakır" },
            new City { Id = Guid.NewGuid(), Name = "Hatay" },
            new City { Id = Guid.NewGuid(), Name = "Kayseri" },
            new City { Id = Guid.NewGuid(), Name = "Samsun" },
            new City { Id = Guid.NewGuid(), Name = "Balıkesir" },
            new City { Id = Guid.NewGuid(), Name = "Kahramanmaraş" },
            new City { Id = Guid.NewGuid(), Name = "Van" },
            new City { Id = Guid.NewGuid(), Name = "Aydın" },
            new City { Id = Guid.NewGuid(), Name = "Tekirdağ" }
        };

        context.Cities.AddRange(cities);
        context.SaveChanges();

        
    }

    if (!context.Roles.Any())
    {
        var roles = new List<AppRole>
            {
                new AppRole { Name = "Admin"},
                new AppRole { Name = "User"},
                new AppRole { Name = "User"}

            };
        context.Roles.AddRange(roles);
        context.SaveChanges();
    }

    if (!context.Cargo.Any())
    {
        var cargo = new Cargo
        {
            Id = Guid.NewGuid(),
            SenderId = Guid.Parse("5a4f1a06-bd05-45d3-a9ab-08df0855f6db"),
            ReceiverId = Guid.Parse("15f26dc4-c27f-4832-7ad4-08df09eb55ba"),
            OriginBranchId = Guid.Parse("29a7aa53-0410-46ea-834d-ae5280cd5395"),
            DestinationBranchId = Guid.Parse("29a7aa53-0410-46ea-834d-ae5280cd5395"),
            TrackCode = "CT202609081234",
            ShipmentDate = DateTime.Now,
            EstinatedArrivalDate = DateTime.Now.AddDays(2),
            cargoType = CargoTrack.Entity.Entities.Enums.CargoType.Standart,
            CargoStatus = CargoTrack.Entity.Entities.Enums.CargoStatus.DispatchedFromTransferCenter
        };

        context.Add(cargo);
        context.SaveChanges();
    }
}


app.Run();
