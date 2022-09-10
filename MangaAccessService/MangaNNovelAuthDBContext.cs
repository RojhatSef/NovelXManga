using MangaModelService;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class MangaNNovelAuthDBContext : IdentityDbContext
    {
        public MangaNNovelAuthDBContext(DbContextOptions<MangaNNovelAuthDBContext> options) : base(options)
        {

        }
        public DbSet<ArtistModel> artistModels { get; set; }
        public DbSet<AuthorModel> authorModels { get; set; }
        public DbSet<ChapterModel> chapterModels { get; set; }

        public DbSet<GroupScanlatingModel> groupScanlatingModels { get; set; }

        public DbSet<StudioModel> studioModels { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<MangaModel> mangaModels { get; set; }
        public DbSet<MasterModel> MasterModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MasterModel>()
                .HasKey(m => new
                {
                    m.MangaModelId,
                    m.GroupsSanlatingId,
                    m.postModelID,
                    m.ChapterModelId,
                    m.ArtistModelID,
                    m.AuthorModelID,
                    m.StudioModelID
                });

            modelBuilder.Entity<MasterModel>()
                .HasOne(mm => mm.mangaModel)
                .WithMany(m => m.MasterModels)
                .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<MasterModel>()
                .HasOne(mm => mm.ArtistModel)
                .WithMany(m => m.MasterModels)
                .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<MasterModel>()
               .HasOne(mm => mm.GroupScanlatingModel)
               .WithMany(m => m.MasterModels)
               .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<MasterModel>()
               .HasOne(mm => mm.StudioModel)
               .WithMany(m => m.MasterModels)
               .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<MasterModel>()
               .HasOne(mm => mm.chapterModel)
               .WithMany(m => m.MasterModels)
               .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<MasterModel>()
               .HasOne(mm => mm.PostModel)
               .WithMany(m => m.MasterModels)
               .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<MasterModel>()
               .HasOne(mm => mm.AuthorModel)
               .WithMany(m => m.MasterModels)
               .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ChapterModel>()
                .HasOne(cm => cm.GroupScanlatingModel)
                .WithMany(c => c.chapterModels)
                .HasForeignKey(pt => pt.chapterID).OnDelete(DeleteBehavior.ClientCascade); ;
            base.OnModelCreating(modelBuilder);

        }




    }
}