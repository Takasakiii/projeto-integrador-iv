using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Password).HasMaxLength(92).IsRequired();
        builder.Property(x => x.ImageId).HasMaxLength(32);
        builder.Property(x => x.Description).HasMaxLength(255);
        builder.Property(x => x.Role).HasMaxLength(100);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne(x => x.Image).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}