using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class JobModelConfiguration : IEntityTypeConfiguration<JobModel>
{
    public void Configure(EntityTypeBuilder<JobModel> builder)
    {
        builder.ToTable("Jobs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Value).IsRequired();

        builder.HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}