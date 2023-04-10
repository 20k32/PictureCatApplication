using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace PictureCat
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Category");
            builder
                .Property(c => c.CategoryName)
                .HasColumnType("nvarchar")
                .HasMaxLength(30);

            #region binding categories with images using third table

            builder
                .HasMany(c => c.ImageCategories)
                .WithOne(ic => ic.CategoryEntity)
                .HasForeignKey(ic => ic.CategoryId)
                .HasPrincipalKey(c => c.Id);

            #endregion

            builder.HasData(
                new CategoryEntity[]
                {
                    new CategoryEntity() { Id = 1, CategoryName = "House"},
                    new CategoryEntity() { Id = 2, CategoryName = "Human"},
                    new CategoryEntity() { Id = 3, CategoryName = "Animal"},
                    new CategoryEntity() { Id = 4, CategoryName = "Peisage"},
                    new CategoryEntity() { Id = 5, CategoryName = "Dog"},
                    new CategoryEntity() { Id = 6, CategoryName = "Cat"},
                    new CategoryEntity() { Id = 7, CategoryName = "Fish"},
                    new CategoryEntity() { Id = 8, CategoryName = "Fruit"},
                });
        }
    }

    public class TagConfiguration : IEntityTypeConfiguration<TagEntity>
    {
        public void Configure(EntityTypeBuilder<TagEntity> builder)
        {
            builder.ToTable("Tags");
            builder
                .Property(t => t.TagName)
                .HasColumnType("nvarchar")
                .HasMaxLength(30);

            #region binding tags with images using third table

            builder
                .HasMany(t => t.ImageTags)
                .WithOne(it => it.TagEntity)
                .HasForeignKey(it => it.TagId)
                .HasPrincipalKey(t => t.Id);

            #endregion

            builder.HasData(
                new TagEntity[]
                {
                    new TagEntity() { Id = 1, TagName = "#Me"},
                    new TagEntity() { Id = 2, TagName = "#MyFamily"},
                    new TagEntity() { Id = 3, TagName = "#Friends"},
                    new TagEntity() { Id = 4, TagName = "#Cute"},
                    new TagEntity() { Id = 5, TagName = "#Beauty"},
                    new TagEntity() { Id = 6, TagName = "#Sport"},
                    new TagEntity() { Id = 7, TagName = "#Hobby"},
                    new TagEntity() { Id = 8, TagName = "#Study"},
                });
        }

    }

    public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.ToTable("Images");
            builder
                .Property(i => i.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);
            builder
                .Property(i => i.Path)
                .HasColumnType("nvarchar")
                .HasMaxLength(200);
            builder
                .Property(i => i.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(300);

            builder
                .Property(i => i.ImageBytes)
                .HasColumnType("varbinary(max)");

            builder
                .Property(i => i.ReleaseDate)
                .HasColumnType("date");

            #region binding images with categories using third table

            builder
                .HasMany(i => i.ImageCategories)
                .WithOne(ic => ic.ImageEntity)
                .HasForeignKey(ic => ic.ImageId)
                .HasPrincipalKey(i => i.Id);

            #endregion

            #region binding images with tags using third table

            builder
                .HasMany(i => i.ImageTags)
                .WithOne(it => it.ImageEntity)
                .HasForeignKey(it => it.ImageId)
                .HasPrincipalKey(i => i.Id);

            #endregion
        }
    }

    public class ImageToTagConfiguration : IEntityTypeConfiguration<ImageToTag>
    {
        public void Configure(EntityTypeBuilder<ImageToTag> builder)
        {
            builder.ToTable("ImagesToTags");
            builder.HasKey(itt => new { itt.ImageId, itt.TagId });
        }
    }

    public class ImageToCategoryConfiguration : IEntityTypeConfiguration<ImageToCategory>
    {
        public void Configure(EntityTypeBuilder<ImageToCategory> builder)
        {
            builder.ToTable("ImagesToCategories");
            builder.HasKey(itc => new { itc.ImageId, itc.CategoryId });
        }
    }
}
