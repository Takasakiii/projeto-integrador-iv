using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class ImageModelConfiguration : IEntityTypeConfiguration<ImageModel>
{
    public void Configure(EntityTypeBuilder<ImageModel> builder)
    {
        builder.ToTable("Images");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(32).IsRequired();
        builder.Property(x => x.Mime).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Data).HasMaxLength(4 * 1024 * 1024).IsRequired();
    }
}