using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class JobSkillModelConfiguration : IEntityTypeConfiguration<JobSkillModel>
{
    public void Configure(EntityTypeBuilder<JobSkillModel> builder)
    {
        builder.ToTable("JobSkills");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.JobId).IsRequired();
        builder.Property(x => x.SkillId).IsRequired();
        builder.Property(x => x.Optional).IsRequired();

        builder.HasOne(x => x.Job).WithMany(x => x.Skills).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Skill).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}