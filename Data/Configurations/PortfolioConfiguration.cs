using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {

        builder.ToTable(name: "portfolios", schema: "catalog");

        builder.HasKey(field => field.Id);
        builder.Property(field => field.Id)
                .HasColumnName("id");

        builder.Property(field => field.Title)
                .HasColumnName("title")
                .HasMaxLength(50)
                .IsRequired();

        builder.Property(field => field.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();

        builder.Property(field => field.Image)
                .HasColumnName("image")
                .HasColumnType("text")
                .HasDefaultValue(null);

        builder.Property(field => field.Link)
                .HasColumnName("link")
                .HasColumnType("text")
                .HasDefaultValue(null);

        builder.Property(field => field.Status)
                .HasColumnName("status")
                .HasDefaultValue(false);

        builder.HasOne(field => field.Category)
                .WithMany(field => field.Portfolios)
                .HasForeignKey(field => field.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(field => field.Tools)
                .WithMany(field => field.Portfolios)
                .UsingEntity(j => j.ToTable(name: "portfolios_tools", schema: "catalog"));

    }
}
