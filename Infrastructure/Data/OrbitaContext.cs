using Microsoft.EntityFrameworkCore;
using Orbita.SoaApi.Domain.Entities;

namespace Orbita.SoaApi.Infrastructure.Data
{
    public class OrbitaContext : DbContext
    {
        public OrbitaContext(DbContextOptions<OrbitaContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<CareerPath> CareerPaths => Set<CareerPath>();
        public DbSet<Mission> Missions => Set<Mission>();
        public DbSet<UserMissionProgress> UserMissionProgresses => Set<UserMissionProgress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Name).IsRequired().HasMaxLength(200);
                b.Property(u => u.Email).IsRequired().HasMaxLength(200);
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.PasswordHash).IsRequired();
                b.Property(u => u.Role).HasConversion<int>();
            });

            modelBuilder.Entity<CareerPath>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Name).IsRequired().HasMaxLength(200);
                b.Property(c => c.Area).IsRequired().HasMaxLength(200);
                b.Property(c => c.Description).IsRequired();
                b.Property(c => c.Level).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Mission>(b =>
            {
                b.HasKey(m => m.Id);
                b.Property(m => m.Title).IsRequired().HasMaxLength(200);
                b.Property(m => m.Description).IsRequired();
                b.Property(m => m.Difficulty).IsRequired();
                b.Property(m => m.EstimatedMinutes).IsRequired();
                b.Property(m => m.XpReward).IsRequired();
                b.Property(m => m.Status).HasConversion<int>();
                b.HasOne(m => m.CareerPath).WithMany(c => c.Missions).HasForeignKey(m => m.CareerPathId);
            });

            modelBuilder.Entity<UserMissionProgress>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Status).HasConversion<int>();
                b.HasOne(p => p.User).WithMany(u => u.Progress).HasForeignKey(p => p.UserId);
                b.HasOne(p => p.Mission).WithMany(m => m.UserProgress).HasForeignKey(p => p.MissionId);
            });
        }
    }
}
