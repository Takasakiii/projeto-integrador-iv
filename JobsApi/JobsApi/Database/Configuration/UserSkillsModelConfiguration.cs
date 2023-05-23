using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class UserSkillsModelConfiguration : IEntityTypeConfiguration<UserSkillModel>
{
    public void Configure(EntityTypeBuilder<UserSkillModel> builder)
    {
        builder.ToTable("UsersSkills");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.SkillId).IsRequired();
        builder.Property(x => x.Level).IsRequired();
        builder.Property(x => x.Years).IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.Skills).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Skill).WithMany().OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.UserId, x.SkillId }).IsUnique();
    }
}