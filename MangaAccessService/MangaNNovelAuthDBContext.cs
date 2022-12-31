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
        public DbSet<VoiceActorModel> voiceActorModels { get; set; }
        public DbSet<ChapterModel> chapterModels { get; set; }

        public DbSet<GroupScanlatingModel> groupScanlatingModels { get; set; }
        public DbSet<languages> Languages_ { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<StudioModel> studioModels { get; set; }
        public DbSet<AssociatedNames> AssociatedNames { get; set; }
        public DbSet<PostModel> PostModels { get; set; }
        public DbSet<GenresModel> GenresModels { get; set; }
        public DbSet<TagModel> TagModels { get; set; }
        public DbSet<BlogModel> blogModels { get; set; }
        public DbSet<MangaModel> mangaModels { get; set; }
        public DbSet<MasterModel> MasterModels { get; set; }
        //public DbSet<IdentityUser> IdentityUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterModel>()
           .HasOne(mm => mm.MangaModels)
           .WithOne(i => i.MasterModels)
           .HasForeignKey<MangaModel>(b => b.MasterID);


            modelBuilder.Entity<BlogModel>()
            .HasOne(mm => mm.MangaModel)
            .WithOne(i => i.BlogModel)
            .HasForeignKey<MangaModel>(b => b.BlogModelId);

            modelBuilder.Entity<MangaModel>().Navigation(e => e.Authormodels).AutoInclude();
            modelBuilder.Entity<MangaModel>().Navigation(e => e.TagsModels).AutoInclude();
            modelBuilder.Entity<MangaModel>().Navigation(e => e.GenresModels).AutoInclude();
            modelBuilder.Entity<MangaModel>().Navigation(e => e.ArtistModels).AutoInclude();
            modelBuilder.Entity<MangaModel>().Navigation(e => e.VoiceActors).AutoInclude();

            modelBuilder.Entity<MangaModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.chapterModels).AutoInclude();
            modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.MasterModels).AutoInclude();
            modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.userModels).AutoInclude();
            modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.chapterModels).AutoInclude();
            modelBuilder.Entity<ArtistModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            modelBuilder.Entity<AuthorModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            modelBuilder.Entity<VoiceActorModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            modelBuilder.Entity<BlogModel>().Navigation(e => e.postsModel).AutoInclude();
            modelBuilder.Entity<ChapterModel>().Navigation(e => e.GroupScanlatingModels).AutoInclude();
            modelBuilder.Entity<MasterModel>().Navigation(e => e.GroupScanlating).AutoInclude();
            modelBuilder.Entity<MasterModel>().Navigation(e => e.userModels).AutoInclude();
            modelBuilder.Entity<MasterModel>().Navigation(e => e.MangaModels).AutoInclude();





            base.OnModelCreating(modelBuilder);
            #region A test for other model building

            //modelBuilder.Entity<MasterModel>()
            //    .HasKey(m => new
            //    {
            //        m.MangaModels,
            //        m.GroupScanlating,
            //        m.userModels

            //    });






            //modelBuilder.Entity<MasterModel>()
            //    .HasOne(mm => mm.mangaModel)
            //    .WithMany(m => m.MasterModels)
            //    .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);




            //modelBuilder.Entity<MasterModel>()
            //    .HasOne(mm => mm.ArtistModel)
            //    .WithMany(m => m.MasterModels)
            //    .HasForeignKey(mm => mm.ArtistModelID).OnDelete(DeleteBehavior.ClientCascade);
            //modelBuilder.Entity<MasterModel>()
            //   .HasOne(mm => mm.GroupScanlatingModel)
            //   .WithMany(m => m.MasterModels)
            //   .HasForeignKey(mm => mm.GroupsSanlatingId).OnDelete(DeleteBehavior.ClientCascade);

            //modelBuilder.Entity<MasterModel>()
            //   .HasOne(mm => mm.StudioModel)
            //   .WithMany(m => m.MasterModels)
            //   .HasForeignKey(mm => mm.StudioModelID).OnDelete(DeleteBehavior.ClientCascade);

            //modelBuilder.Entity<MasterModel>()
            //   .HasOne(mm => mm.chapterModel)
            //   .WithMany(m => m.MasterModels)
            //   .HasForeignKey(mm => mm.ChapterModelId).OnDelete(DeleteBehavior.ClientCascade);



            //modelBuilder.Entity<MasterModel>()
            //   .HasOne(mm => mm.PostModel)
            //   .WithMany(m => m.MasterModels)
            //   .HasForeignKey(mm => mm.MangaModelId).OnDelete(DeleteBehavior.ClientCascade);

            //modelBuilder.Entity<MasterModel>()
            //   .HasOne(mm => mm.AuthorModel)
            //   .WithMany(m => m.MasterModels)
            //   .HasForeignKey(mm => mm.AuthorModelID).OnDelete(DeleteBehavior.ClientCascade);


            //modelBuilder.Entity<GroupScanlatingModel>()
            //    .HasMany(c => c.chapterModels)
            //    .WithMany(gm => gm.GroupScanlatingModels)
            //    .using

            //modelBuilder.Entity<ChapterModel>()
            //    .HasOne(cm => cm.GroupScanlatingModels)
            //    .WithMany(c => c.chapterModels)
            //    .HasForeignKey(pt => pt.GroupScanlatingID).OnDelete(DeleteBehavior.ClientCascade); ;

            //modelBuilder.Entity<PostModel>()
            //    .HasOne(p => p.BlogModel)
            //    .WithMany(b => b.postsModel)
            //    .HasForeignKey(pm => pm.BlogId);

            #endregion

        }




    }
}