using EmailService;
using MangaAccessService;
using MangaAccessService.Migrations;
using MangaModelService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NovelXManga;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US") };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddDbContext<MangaNNovelAuthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"),
        b => b.MigrationsAssembly("MangaAccessService")));

builder.Services.AddScoped<IGenreRepsitory, SQLGenreRepsitory>();
builder.Services.AddScoped<ITagRepsitory, SQLTagRepository>();
builder.Services.AddScoped<IAuthorRepsitory, SQLAuthorRepository>();
builder.Services.AddScoped<IVoiceRepsitory, SQLVoiceRepsitorycs>();
builder.Services.AddScoped<IArtistRepsitory, SQLArtistRepsitory>();
builder.Services.AddScoped<IWallPaperRepsitory, SQLWallPaperRepsitory>();
builder.Services.AddScoped<IStudioRepsitory, SQLStudioRepsitory>();
builder.Services.AddScoped<ILanguageRepsitory, SQLLanguageRepsitory>();
builder.Services.AddScoped<IMangaRepository, SQLMangaRepository>();
builder.Services.AddScoped<IBlogRepsitory, SQLBlogRepsitory>();
builder.Services.AddScoped<IChapterModelRepsitory, SQLChapterModelRepsitory>();
builder.Services.AddScoped<IPostRepsitory, SQLPostRepsitory>();
builder.Services.AddScoped<IAssociatedNamesRepsitory, SQLAssociatedNamesRepsitory>();
builder.Services.AddScoped<IReviewRepsitory, SQLReviewRepsitorycs>();
builder.Services.AddScoped<ICharacterRepsitory, SQLCharacterRepository>();
builder.Services.AddScoped<IOfficalWebsiteRepsitory, SQLOfficalWebsiteRepsitory>();
builder.Services.AddScoped<IReadingListPageRepsitory, SQLReadingListPageRepsitory>();
builder.Services.AddScoped<IFavoritListPageRepsitory, SQLFavoritListPageRepsitory>();
builder.Services.AddScoped<IBuyPageRepsitory, SQLBuyPageRepsitory>();
builder.Services.AddScoped<IDroppedBookListPageRepsitory, SQLDroppedBookListPageRepsitory>();

builder.Services.AddScoped<SeedData>();
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 2;
}).AddEntityFrameworkStores<MangaNNovelAuthDBContext>().AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login/LoginIndex";
});
builder.Services.AddScoped<MangaRankingService>();
builder.Services.AddHostedService<UpdateRankingsBackgroundService>();

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
app.UseRequestLocalization();

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
// fix a forum? <Unsure if we want this> everymanga field is a forumish Done. Chat Will be done on a mangapage instead of using this.
// Fix private chatrooms?
// fix Message

//todo
//User profile,

// fix  Post/forum With comments
//fix photopath Done
// fix double loggin problem done
// fix adding publishers
// selling Point Book Of the week/ book of the month // book CLUB
// fix returnURl if user is not allowed on webpage.
// fix userRoles and users
// add Roles to users, Done
// fix Updates work correctly, 50% done
// fix the GroupScanlation Adds users /Manga Will not be done, as the page wont have scanlations.
// fix Manga to users / group Done.
// fix User to Manga and Group,
// Fix deleting commets // comments can't be delted
// Fix Update Comments // replies

// fix removing users 60% done

// fix  tags and genres Done
// link genres to manga Done
// link tags  to manga Done
// search filter working with tags, name, genres, language etc
// Display Mangas Done
// Display currentManga Done
// Fix manga links to Author/Artist/Voice To their info Done
// fix tags/genre/language to search path ? unsure what i wanted here lol
// fix IBuyPage SQLBuyPage  Done
// Fix Scope for Buypage. Done

// 