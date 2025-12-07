using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

        builder.ToTable(name: "categories", schema: "catalog");

        builder.HasKey(field => field.Id);
        builder.Property(field => field.Id)
                .HasColumnName("id");

        builder.HasIndex(field => field.Name)
                .IsUnique();
        builder.Property(field => field.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

    }
}
