using MangaAccessService;
using MangaAccessService.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NovelXManga;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MangaNNovelAuthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"),
        b => b.MigrationsAssembly("MangaAccessService")));

builder.Services.AddScoped<IGenreRepsitory, SQLGenreRepsitory>();
builder.Services.AddScoped<ITagRepsitory, SQLTagRepository>();
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
//missing repo
//studio and BuyPage
// make or break ToDos
// Sync all Data for every download page.

//large ToDos
// fix a forum? <Unsure if we want this> everymanga field is a forumish Done
// Fix private chatrooms?
// fix Message

//todo
//fix photopath
// fix double loggin problem
// fix adding publishers
// selling Point Book Of the week/ book of the month // book CLUB
// fix returnURl
// fix userRoles and users
// add Roles to users,
// fix Updates work correctly
// fix the GroupScanlation Adds users /Manga
// fix Master to users / group
// fix User to Master and Group
// Fix deleting commets
// Fix Update Comments
// fix blog With comments
// fix removing users
// fix  tags and genres Done
// link genres to manga Done
// link tags  to manga Done
// Display Mangas Done
// Display currentManga Done
// Fix manga links to Author/Artist/Voice To their info
// fix tags/genre/language to search path
// fix IBuyPage SQLBuyPage
// Fix Scope for Buypage.

// 