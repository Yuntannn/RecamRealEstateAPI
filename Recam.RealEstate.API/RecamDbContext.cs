using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API
{
    public class RecamDbContext:IdentityDbContext<User>
    {
        public RecamDbContext(DbContextOptions<RecamDbContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<ListingCase> ListingCases { get; set; }
        public DbSet<MediaAsset> MediaAssets { get; set; }
        public DbSet<StatusHistory> StatusHistories { get; set; }
        public DbSet<SelectMedia> SelectMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(u => u.Name).IsUnique();
                u.Property(u => u.Name).IsRequired().HasMaxLength(100);
                u.Property(u => u.UserRole).IsRequired();
            });

            modelBuilder.Entity<ListingCase>(l =>
            {
                // Add fluent API override to explain multiple navigation properties to the same entity User
                l.HasOne(l => l.Creator).WithMany(l => l.CreatedListings).HasForeignKey(l => l.CreateById).OnDelete(DeleteBehavior.Restrict);
                l.HasOne(l => l.Assignee).WithMany(l => l.AssignedListings).HasForeignKey(l => l.AssignedToId).OnDelete(DeleteBehavior.Restrict);

                l.Property(l => l.Address).IsRequired().HasMaxLength(200);
                l.Property(l => l.PropertyType).IsRequired().HasConversion<string>();
                l.Property(l => l.PropertyStatus).IsRequired().HasConversion<string>();
            });

            modelBuilder.Entity<MediaAsset>(m =>
            {
                m.Property(m => m.MediaType).IsRequired().HasConversion<string>();
                m.HasIndex(m => m.FileUrl).IsUnique(); // Ensure the same file won't be uploaded for multiple times
            });

            modelBuilder.Entity<StatusHistory>(s =>
            {
                s.Property(s => s.OldStatus).IsRequired().HasConversion<string>(); // Both old and new status need to be verified even if the old status at the beginning is none.
                s.Property(s => s.NewStatus).IsRequired().HasConversion<string>();
            });

            modelBuilder.Entity<SelectMedia>(s =>
            {
                s.HasOne(s => s.MediaAsset).WithMany().HasForeignKey(sm => sm.MediaAssetId).OnDelete(DeleteBehavior.Restrict);
                s.HasOne(s => s.ListingCase).WithMany().HasForeignKey(sm => sm.ListingCaseId).OnDelete(DeleteBehavior.Restrict);
                s.HasOne(s => s.Agent).WithMany().HasForeignKey(sm => sm.SelectById).OnDelete(DeleteBehavior.Restrict);
            });




        }
    }
}
