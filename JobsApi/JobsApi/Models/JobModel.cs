namespace JobsApi.Models;

public class JobModel
{
    public JobModel(string title, string description, uint userId, uint value, JobModelLevel level)
    {
        Title = title;
        Description = description;
        UserId = userId;
        Value = value;
        Level = level;
    }

    public uint Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public uint UserId { get; set; }
    public uint Value { get; set; }
    public JobModelLevel Level { get; set; }

    public UserModel? User { get; set; }
    public IEnumerable<JobSkillModel>? Skills { get; set; }
}