namespace JobsApi.Models;

public class WorkModel
{
    public WorkModel(string title, string description, DateTimeOffset startAt, uint userId, uint value)
    {
        Title = title;
        Description = description;
        StartAt = startAt;
        UserId = userId;
        Value = value;
    }

    public uint Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset StartAt { get; set; }
    public DateTimeOffset? EndAt { get; set; }
    public uint Value { get; set; }
    
    public uint UserId { get; set; }
    public UserModel? User { get; set; }
    public IEnumerable<WorkSkillModel>? Skills { get; set; }
}