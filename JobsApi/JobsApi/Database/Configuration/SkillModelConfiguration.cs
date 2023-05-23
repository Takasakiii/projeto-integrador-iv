using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class SkillModelConfiguration : IEntityTypeConfiguration<SkillModel>
{
    public void Configure(EntityTypeBuilder<SkillModel> builder)
    {
        builder.ToTable("Skills");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
    }
}