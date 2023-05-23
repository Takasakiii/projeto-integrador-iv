using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class WorkSkillModelConfiguration : IEntityTypeConfiguration<WorkSkillModel>
{
    public void Configure(EntityTypeBuilder<WorkSkillModel> builder)
    {
        builder.ToTable("WorksSkills");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.WorkId).IsRequired();
        builder.Property(x => x.SkillId).IsRequired();

        builder.HasIndex(x => new { x.WorkId, x.SkillId }).IsUnique();

        builder.HasOne(x => x.Work).WithMany(x => x.Skills).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Skill).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}