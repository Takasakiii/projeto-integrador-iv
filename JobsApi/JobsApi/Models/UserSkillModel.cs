namespace JobsApi.Models;

public class UserSkillModel
{
    public UserSkillModel(uint userId, uint skillId, UserSkillsModelLevel level, uint years)
    {
        UserId = userId;
        SkillId = skillId;
        Level = level;
        Years = years;
    }

    public uint Id { get; set; }
    public uint UserId { get; set; }
    public uint SkillId { get; set; }
    public UserSkillsModelLevel Level { get; set; }
    public uint Years { get; set; }
    
    public UserModel? User { get; set; }
    public SkillModel? Skill { get; set; }
}