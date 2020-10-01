using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatingApp.API.Database
{
    public partial class DatingAppContext : DbContext
    {
        public DatingAppContext()
        {
        }

        public DatingAppContext(DbContextOptions<DatingAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Value> Value { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=DatingApp;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PublicId).HasColumnName("PublicID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.DateOfBirth).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.LastActive).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

            modelBuilder.Entity<Value>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
