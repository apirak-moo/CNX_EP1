using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ToolConfiguration : IEntityTypeConfiguration<Tool>
{
    public void Configure(EntityTypeBuilder<Tool> builder)
    {

        builder.ToTable(name: "tools", schema: "catalog");

        builder.HasKey(field => field.Id);
        builder.Property(field => field.Id)
                .HasColumnName("id");

        builder.HasIndex(field => field.Name)
                .IsUnique();
        builder.Property(field => field.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

        builder.Property(field => field.Logo)
                .HasColumnName("logo")
                .HasColumnType("text")
                .HasDefaultValue(null);

    }
}
