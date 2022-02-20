namespace FootballManager.Data
{
    using FootballManager.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : DbContext
    {
        public FootballManagerDbContext()
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<UserPlayer> UserPlayers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server =.; Database = FootballManager; User Id = ; Password = ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlayer>(e =>
            {
                e.HasKey(up => new { up.UserId, up.PlayerId });

                e
                    .HasOne(up => up.User)
                    .WithMany(up => up.UserPlayers)
                    .HasForeignKey(op => op.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e
                    .HasOne(up => up.Player)
                    .WithMany(up => up.UserPlayers)
                    .HasForeignKey(up => up.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
