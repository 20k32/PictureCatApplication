using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System.IO;
using System.Windows;

namespace PictureCat
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ImageEntity> Images { get; set; } = null!;
        public DbSet<CategoryEntity> Categories { get; set; } = null!;
        public DbSet<TagEntity> Tags { get; set; } = null!;
        public DbSet<ImageToCategory> ImagesToCategories { get; set; } = null!;
        public DbSet<ImageToTag> ImagesToTags { get; set; } = null!;

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;AttachDbFileName=" + Path.Combine(Helper.CurrentDirectory, "PictureCatDB.mdf") + ";Database=PictureCatDB;Trusted_Connection=True"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImageToTagConfiguration());
            modelBuilder.ApplyConfiguration(new ImageToCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
        }

        public static ApplicationDbContext GetInstance()
        {
            return ApplicatonDbContextInstance.Instance;
        }

        private static class ApplicatonDbContextInstance
        {
            public static readonly ApplicationDbContext Instance = new ApplicationDbContext();
            static ApplicatonDbContextInstance() { }
        }
    }
}
