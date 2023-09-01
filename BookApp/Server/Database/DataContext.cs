namespace BookApp.Server.Database
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<BookAnalysis> BookAnalyses { get; set; }
        public DbSet<Highlight> Highlights { get; set; }


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

            ////Database Seed
            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, Email = "test@test.com", PasswordHash = "123", UserName = "TestUser1" },
            //    new User { Id = 2, Email = "test@test.com", PasswordHash = "123", UserName = "TestUser2" },
            //    new User { Id = 3, Email = "test@test.com", PasswordHash = "123", UserName = "TestUser3" }
            //);

            ////Database Configuration
            modelBuilder.Entity<BookAnalysis>().HasKey(b => b.Id);

            modelBuilder.Entity<BookAnalysis>()
                .HasMany(b => b.Highlights)
                .WithOne(h => h.BookAnalysis)
                .HasForeignKey(h => h.BookAnalysisId);

            modelBuilder.Entity<Highlight>().HasKey(b => b.Id);

            //modelBuilder.Entity<PantryUser>().HasKey(u => new { u.UserId, u.PantryId });

            //modelBuilder.Entity<PantryUser>()
            //    .HasOne(t => t.User)
            //    .WithMany(t => t.PantryUser)
            //    .HasForeignKey(t => t.UserId);

            //modelBuilder.Entity<PantryUser>()
            //    .HasOne(t => t.Pantry)
            //    .WithMany(t => t.PantryUser)
            //    .HasForeignKey(t => t.PantryId);

            //modelBuilder.Entity<CategoryProduct>().HasKey(u => new { u.CategoryId, u.ProductId });

            //modelBuilder.Entity<CategoryProduct>()
            //    .HasOne(t => t.Category)
            //    .WithMany(t => t.CategoryProduct)
            //    .HasForeignKey(t => t.CategoryId);

            //modelBuilder.Entity<CategoryProduct>()
            //    .HasOne(t => t.Product)
            //    .WithMany(t => t.CategoryProduct)
            //    .HasForeignKey(t => t.ProductId);

            //modelBuilder.Entity<PantryInvite>()
            //    .HasOne(t => t.Reciever)
            //    .WithMany(t => t.RecievedPantryInvites)
            //    .HasForeignKey(t => t.RecieverId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<PantryInvite>()
            //    .HasOne(t => t.Sender)
            //    .WithMany(t => t.SentPantryInvites)
            //    .HasForeignKey(t => t.SenderId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
