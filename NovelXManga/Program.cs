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
SeedDatainitialize(app);


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
static void SeedDatainitialize(IHost host)
{
    var scopefactorty = host.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopefactorty.CreateScope())
    {
        var seed = scope.ServiceProvider.GetService<SeedData>();
        seed.seedData().Wait();
    }
}

//todo
//fix photopath
// fix returnURl
// fix userRoles and users 
// add Roles to users, 
// fix Updates work correctly 
// fix removing users
// fix  tags and genres Done
// link genres to manga Done 
// link tags  to manga Done 

// 