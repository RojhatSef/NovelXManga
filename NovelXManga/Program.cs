using MangaAccessService;
using MangaAccessService.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NovelXManga;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MangaNNovelAuthDBContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("AuthConnectionString")));


builder.Services.AddScoped<IAuthorRepsitory, SQLAuthorRepository>();
builder.Services.AddScoped<IMangaRepository, SQLMangaRepository>();
builder.Services.AddScoped<SeedData>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 2;
}).AddEntityFrameworkStores<MangaNNovelAuthDBContext>();


builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login/LoginIndex";
});
var app = builder.Build();
database();


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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
void database()
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<SeedData>();
        seeder.seedData();
    }
}