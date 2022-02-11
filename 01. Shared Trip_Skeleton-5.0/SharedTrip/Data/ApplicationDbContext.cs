namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }

        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(e =>
            {
                e.HasKey(ut => new { ut.UserId, ut.TripId });

                e
                    .HasOne(ut => ut.User)
                    .WithMany(ut => ut.UserTrips)
                    .HasForeignKey(op => op.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e
                    .HasOne(ut => ut.Trip)
                    .WithMany(ut => ut.UserTrips)
                    .HasForeignKey(op => op.TripId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
