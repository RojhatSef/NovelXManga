using EmailService;
using MangaAccessService;
using MangaAccessService.Migrations;
using MangaModelService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NovelXManga;
using NovelXManga.Pages.UserInteractions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxConcurrentConnections = 100; // Adjust based on expected user load
    serverOptions.Limits.MaxConcurrentUpgradedConnections = 100; // Adjust if using WebSockets
    serverOptions.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB, to handle large file uploads
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5); // Keep alive for 5 minutes to maintain active connections
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2); // Header timeout of 2 minutes
    serverOptions.Limits.Http2.MaxStreamsPerConnection = 100; // Adjust based on expected load and HTTP/2 usage
    serverOptions.Limits.Http2.MaxFrameSize = 16 * 1024; // Default 16 KB, usually sufficient
    serverOptions.Limits.Http2.InitialConnectionWindowSize = 128 * 1024; // 128 KB for initial connection window
    serverOptions.Limits.Http2.InitialStreamWindowSize = 128 * 1024; // 128 KB for initial stream window
});

// Add services to the container.
builder.Services.AddRazorPages();
//Remove if not needed - Remove to text below
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddHttpContextAccessor();
// Remove - End here

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
//builder.Services.AddDbContext<MangaNNovelAuthDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"),
//        b =>
//        {
//            b.MigrationsAssembly("MangaAccessService");
//            b.CommandTimeout(120); // set timeout to 120 seconds
//        }));

builder.Services.AddScoped<IGenreRepsitory, SQLGenreRepsitory>();
builder.Services.AddScoped<ITagRepsitory, SQLTagRepository>();
builder.Services.AddScoped<IAuthorRepsitory, SQLAuthorRepository>();
builder.Services.AddScoped<IVoiceRepsitory, SQLVoiceRepsitorycs>();
builder.Services.AddScoped<IArtistRepsitory, SQLArtistRepsitory>();
builder.Services.AddScoped<IWallPaperRepsitory, SQLWallPaperRepsitory>();
builder.Services.AddScoped<IStudioRepsitory, SQLStudioRepsitory>();
builder.Services.AddScoped<IChapterModelRepsitory, SQLChapterModelRepsitory>();
builder.Services.AddScoped<IMangaRepository, SQLMangaRepository>();
builder.Services.AddScoped<IBlogRepsitory, SQLBlogRepsitory>();
builder.Services.AddScoped<ICharacterRepsitory, SQLCharacterRepository>();
builder.Services.AddScoped<IAssociatedNamesRepsitory, SQLAssociatedNamesRepsitory>();
//builder.Services.AddScoped<IScoreDistributionEntry, SQLScoreDistributionEntry>();
builder.Services.AddScoped<IChapterContentRepository, SQLChapterContentRepository>();
builder.Services.AddScoped<IChapterImageRepository, SQLChapterImageRepository>();

builder.Services.AddScoped<IAttachmentRepository, SQLAttachmentRepository>();
builder.Services.AddScoped<IUserBlockRepository, SQLUserBlockRepository>();
builder.Services.AddScoped<IReportUpdateLogRepository, SQLReportUpdateLogRepository>();
builder.Services.AddScoped<IUserSettingsRepository, SQLUserSettingsRepository>();
builder.Services.AddScoped<IReportAssignmentRepository, SQLReportAssignmentRepository>();
builder.Services.AddScoped<IPrivateMessageRepository, SQLPrivateMessageRepository>();
builder.Services.AddScoped<IPrivateMessageReportRepository, SQLPrivateMessageReportRepository>();
builder.Services.AddScoped<IPrivateConversationRepository, SQLPrivateConversationRepository>();

builder.Services.AddScoped<IReviewRepsitory, SQLReviewRepsitorycs>();
builder.Services.AddScoped<IPostRepsitory, SQLPostRepsitory>();
builder.Services.AddScoped<CheckUserSettings>();
builder.Services.AddScoped<UserBookListService>();
builder.Services.AddScoped<ILanguageRepsitory, SQLLanguageRepsitory>();
builder.Services.AddScoped<IOfficalWebsiteRepsitory, SQLOfficalWebsiteRepsitory>();
builder.Services.AddScoped<IBuyPageRepsitory, SQLBuyPageRepsitory>();
builder.Services.AddScoped<IReadingListPageRepsitory, SQLReadingListPageRepsitory>();
builder.Services.AddScoped<IWishListRepository, SQLWishListRepository>();
builder.Services.AddScoped<ICompletedBookListPageRepsitory, SQLCompletedBookListPageRepsitory>();
builder.Services.AddScoped<IFavoritListPageRepsitory, SQLFavoritListPageRepsitory>();
builder.Services.AddScoped<IDroppedBookListPageRepsitory, SQLDroppedBookListPageRepsitory>();

builder.Services.AddScoped<SeedData>();

builder.Services.AddHttpClient("configured-client", client =>
{
    client.Timeout = TimeSpan.FromMilliseconds(500);
});
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 2;
}).AddEntityFrameworkStores<MangaNNovelAuthDBContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
});
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
builder.Services.AddHostedService<MangaReadResetService>();
var app = builder.Build();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    await next();
});
SeedDatainitialize(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();  // remove if not needed Used for the session with above code.
app.UseMiddleware<RateLimitMiddleware>(TimeSpan.FromSeconds(60));
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//new for hosting
//app.Run("http://0.0.0.0:5000");
//old for local
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