using BookApp.Server.Models.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookApp.Server.Database
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IApiUserService _apiUserService;

        public DataContext(DbContextOptions<DataContext> options, IApiUserService apiUserService) : base(options)
        {
            _apiUserService = apiUserService;
        }

        public DbSet<BookAnalysis> BookAnalyses { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("BookAppDBConnection");
            optionsBuilder.UseSqlServer(connectionString);
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
            modelBuilder.Entity<BookAnalysis>().HasKey(b => b.Id);

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.Highlights)
                .WithOne(h => h.BookAnalysis)
                .HasForeignKey(h => h.BookAnalysisId);

            modelBuilder.Entity<Highlight>().HasKey(b => b.Id);

            modelBuilder.Entity<Highlight>()
                .HasMany(h => h.Tags)
                .WithMany(t => t.Highlights);

            

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

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
