namespace JobsApi.Models;

public class JobSkillModel
{
    public JobSkillModel(uint jobId, uint skillId, bool optional)
    {
        JobId = jobId;
        SkillId = skillId;
        Optional = optional;
    }

    public uint Id { get; set; }
    public uint JobId { get; set; }
    public uint SkillId { get; set; }
    public bool Optional { get; set; }

    public JobModel? Job { get; set; }
    public SkillModel? Skill { get; set; }
}