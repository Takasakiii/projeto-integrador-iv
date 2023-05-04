namespace JobsApi.Models;

public class SkillModel
{
    public SkillModel(string name)
    {
        Name = name;
    }

    public uint Id { get; set; }
    public string Name { get; set; }
}