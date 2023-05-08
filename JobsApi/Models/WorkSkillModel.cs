namespace JobsApi.Models;

public class WorkSkillModel
{
    public WorkSkillModel(uint workId, uint skillId)
    {
        WorkId = workId;
        SkillId = skillId;
    }

    public uint Id { get; set; }
    public uint WorkId { get; set; }
    public uint SkillId { get; set; }

    public WorkModel? Work { get; set; }
    public SkillModel? Skill { get; set; }
}