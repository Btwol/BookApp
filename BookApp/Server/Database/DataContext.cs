using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookApp.Server.Database
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IAppUserService _apiUserService;

        public DataContext(DbContextOptions<DataContext> options, IAppUserService apiUserService) : base(options)
        {
            _apiUserService = apiUserService;
        }

        public DbSet<BookAnalysis> BookAnalyses { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<HighlightNote> HighlightNotes { get; set; }
        public DbSet<ParagraphNote> ParagraphNotes { get; set; }
        public DbSet<AnalysisNote> AnalysisNotes { get; set; }
        public DbSet<ChapterNote> ChapterNotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("BookAppDBConnection");
            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { Id = 1, UserName = "PlaceholderAdmin" },
                new AppUser { Id = 2, UserName = "PlaceholderUser" }
            );

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
                );

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<int> { RoleId = 2, UserId = 2 }
                );

            ////Database Configuration

            modelBuilder.Entity<Highlight>().HasKey(b => b.Id);

            //modelBuilder.Entity<Highlight>()
            //    .HasMany(b => b.Tags)
            //    .WithMany(u => u.Highlights)
            //    .UsingEntity<HighlightTag>();

            modelBuilder.Entity<Highlight>()
                .HasMany(h => h.Tags)
                .WithMany(t => t.Highlights)
                .UsingEntity<Dictionary<string, object>>(
            "HighlightTag",
            x => x.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.NoAction),
            x => x.HasOne<Highlight>().WithMany().OnDelete(DeleteBehavior.NoAction)
        );

            modelBuilder.Entity<Tag>().HasKey(b => b.Id);

            modelBuilder.Entity<BookAnalysis>().HasKey(b => b.Id);

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.Users)
                .WithMany(u => u.BookAnalyses)
                .UsingEntity<BookAnalysisUser>();

            modelBuilder.Entity<BookAnalysis>()
    .HasMany(b => b.Tags)
    .WithOne(n => n.BookAnalysis)
    .HasForeignKey(n => n.BookAnalysisId)
    .OnDelete(DeleteBehavior.Cascade)
    .IsRequired(false);

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.Highlights)
                .WithOne(h => h.BookAnalysis)
                .HasForeignKey(h => h.BookAnalysisId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);


            modelBuilder.Entity<AnalysisNote>()
                .HasMany(h => h.Tags)
                .WithMany(t => t.AnalysisNotes)
                .UsingEntity<Dictionary<string, object>>(
            "AnalysisNoteTag",
            x => x.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.NoAction),
            x => x.HasOne<AnalysisNote>().WithMany().OnDelete(DeleteBehavior.NoAction)
        );
            modelBuilder.Entity<ChapterNote>()
                .HasMany(h => h.Tags)
                .WithMany(t => t.ChapterNotes)
                .UsingEntity<Dictionary<string, object>>(
            "ChapterNoteTag",
            x => x.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.NoAction),
            x => x.HasOne<ChapterNote>().WithMany().OnDelete(DeleteBehavior.NoAction)
        );

            modelBuilder.Entity<ParagraphNote>()
                .HasMany(h => h.Tags)
                .WithMany(t => t.ParagraphNotes)
                .UsingEntity<Dictionary<string, object>>(
            "ParagraphNoteTag",
            x => x.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.NoAction),
            x => x.HasOne<ParagraphNote>().WithMany().OnDelete(DeleteBehavior.NoAction)
        );

            modelBuilder.Entity<HighlightNote>()
                .HasMany(h => h.Tags)
                .WithMany(t => t.HighlightNotes)
                .UsingEntity<Dictionary<string, object>>(
            "HighlightNoteTags",
            x => x.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.NoAction),
            x => x.HasOne<HighlightNote>().WithMany().OnDelete(DeleteBehavior.NoAction)
        );

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.ParagraphNotes)
                .WithOne(n => n.BookAnalysis)
                .HasForeignKey(n => n.BookAnalysisId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.AnalysisNotes)
                .WithOne(n => n.BookAnalysis)
                .HasForeignKey(n => n.BookAnalysisId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.ChapterNotes)
                .WithOne(n => n.BookAnalysis)
                .HasForeignKey(n => n.BookAnalysisId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<Highlight>()
                .HasMany(h => h.HighlightNotes)
                .WithOne(n => n.Highlight)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HighlightNote>()
                .HasOne(n => n.Highlight)
                .WithMany(h => h.HighlightNotes)
                .HasForeignKey(n => n.HighlightId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<Creatable> entry in ChangeTracker.Entries<Creatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatorId = _apiUserService.GetCurrentUserId();
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                }
            }

            foreach (EntityEntry<Updatable> entry in ChangeTracker.Entries<Updatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdaterId = _apiUserService.GetCurrentUserId();
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            foreach (EntityEntry<BookAnalysisUser> entry in ChangeTracker.Entries<BookAnalysisUser>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.MemberType = MemberType.Administrator;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
