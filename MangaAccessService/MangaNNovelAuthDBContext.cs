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
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReadingList> readingLists { get; set; }
        public DbSet<CompletedBookList> completedBookLists { get; set; }
        public DbSet<DroppedBookList> droppedBookLists { get; set; }
        public DbSet<WishBookList> wishBookLists { get; set; }
        public DbSet<FavoritBookList> favoritBookLists { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<GroupScanlatingModel> groupScanlatingModels { get; set; }
        public DbSet<Languages> Languages_ { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<OfficalWebsite> OfficalWebsites { get; set; }
        public DbSet<StudioModel> studioModels { get; set; }
        public DbSet<AssociatedNames> AssociatedNames { get; set; }
        public DbSet<PostModel> PostModels { get; set; }
        public DbSet<GenresModel> GenresModels { get; set; }
        public DbSet<TagModel> TagModels { get; set; }
        public DbSet<BlogModel> blogModels { get; set; }
        public DbSet<MangaModel> mangaModels { get; set; }
        public DbSet<BuyPage> BuyPages { get; set; }

        public DbSet<WallPapers> WallPapers { get; set; }

        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<UserBlock> UserBlocks { get; set; }
        public DbSet<ReportUpdateLog> ReportUpdateLogs { get; set; }
        public DbSet<ReportAssignment> ReportAssignments { get; set; }
        public DbSet<PrivateMessageReport> PrivateMessageReports { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<PrivateConversation> PrivateConversations { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        //public DbSet<TestModel> TestModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configures a one-to-one relationship between BlogModel and MangaModel.
            modelBuilder.Entity<BlogModel>()
            .HasOne(mm => mm.MangaModel)
            .WithOne(i => i.BlogModel)
            .HasForeignKey<MangaModel>(b => b.BlogModelId);
            // Configures a one-to-many relationship between UserModel and PrivateConversation as UserOne.
            // Also prevents cascade deletion to avoid deleting conversations when the UserOne is deleted.
            modelBuilder.Entity<UserModel>()
       .HasMany(u => u.ConversationsAsUserOne)
       .WithOne(c => c.UserOne)
       .HasForeignKey(c => c.UserOneId)
       .OnDelete(DeleteBehavior.Restrict);

            // Configures a one-to-many relationship between UserModel and PrivateConversation as UserTwo.
            // Also prevents cascade deletion to avoid deleting conversations when the UserTwo is deleted.
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.ConversationsAsUserTwo)
                .WithOne(c => c.UserTwo)
                .HasForeignKey(c => c.UserTwoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configures a many-to-one relationship between PrivateMessage and UserModel for Sender.
            // Prevents cascade deletion to avoid deleting messages when the sender is deleted.
            modelBuilder.Entity<PrivateMessage>()
       .HasOne(pm => pm.Sender)
       .WithMany() // assuming Sender is navigation property in PrivateMessage
       .HasForeignKey(pm => pm.SenderId)
       .OnDelete(DeleteBehavior.Restrict);

            // Configures a many-to-one relationship between PrivateMessage and UserModel for Receiver.
            // Prevents cascade deletion to avoid deleting messages when the receiver is deleted.
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(pm => pm.Receiver)
                .WithMany() // assuming Receiver is navigation property in PrivateMessage
                .HasForeignKey(pm => pm.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configures a one-to-many relationship between UserBlock and UserModel for Blocker.
            // Prevents cascade deletion to avoid deleting blocks when the blocker is deleted.
            // Configure UserBlock relationship with Blocker
            modelBuilder.Entity<UserBlock>()
                  .HasOne(ub => ub.Blocker)
                  .WithMany(u => u.BlockedUsers)
                  .HasForeignKey(ub => ub.BlockerId)
                  .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            // Configure UserBlock relationship with BlockedUser
            modelBuilder.Entity<UserBlock>()
                .HasOne(ub => ub.BlockedUser)
                .WithMany(u => u.BlockedByUsers)
                .HasForeignKey(ub => ub.BlockedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configures a many-to-one relationship between ReportAssignment and UserModel for Admin.
            // Prevents cascade deletion to avoid deleting assignments when the admin is deleted.
            modelBuilder.Entity<ReportAssignment>()
        .HasOne(ra => ra.Admin)
        .WithMany()
        .HasForeignKey(ra => ra.AdminId)
        .OnDelete(DeleteBehavior.Restrict);

            // Configures a one-to-many relationship between ReportAssignment and PrivateMessageReport.
            // Prevents cascade deletion to avoid deleting assignments when the report is deleted.
            modelBuilder.Entity<ReportAssignment>()
                .HasOne(ra => ra.Report)
                .WithMany(r => r.Assignments)
                .HasForeignKey(ra => ra.ReportId)
                .OnDelete(DeleteBehavior.Restrict);
            // There is no navigation property back to UserBlock from UserModel for BlockedUser.
            // Prevents cascade deletion to avoid deleting blocks when the blocked user is deleted.
            modelBuilder.Entity<UserBlock>()
                .HasOne(ub => ub.BlockedUser)
                .WithMany() // No navigation property back to UserBlock from UserModel for BlockedUser
                .HasForeignKey(ub => ub.BlockedUserId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);

            #region AutoNavagtions not needed

            //modelBuilder.Entity<MangaModel>().Navigation(e => e.Authormodels).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.TagsModels).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.GenresModels).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.ArtistModels).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.VoiceActors).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.AllLanguages).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.StudioModels).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.BlogModel).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.OfficalWebsites).AutoInclude();
            //modelBuilder.Entity<MangaModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            //modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.chapterModels).AutoInclude();
            //modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.userModels).AutoInclude();
            //modelBuilder.Entity<GroupScanlatingModel>().Navigation(e => e.chapterModels).AutoInclude();
            //modelBuilder.Entity<ArtistModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            //modelBuilder.Entity<ArtistModel>().Navigation(e => e.OfficalWebsites).AutoInclude();
            //modelBuilder.Entity<AuthorModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            //modelBuilder.Entity<VoiceActorModel>().Navigation(e => e.AssociatedNames).AutoInclude();
            //modelBuilder.Entity<VoiceActorModel>().Navigation(e => e.OfficalWebsites).AutoInclude();

            //modelBuilder.Entity<UserModel>().Navigation(e => e.Reviews).AutoInclude();

            #endregion AutoNavagtions not needed
        }
    }
}