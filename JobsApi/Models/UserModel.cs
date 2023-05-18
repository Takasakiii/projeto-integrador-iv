namespace JobsApi.Models;

public class UserModel
{
    public UserModel(string name, string email, UserModelType type, string password)
    {
        Name = name;
        Email = email;
        Type = type;
        Password = password;
    }

    public uint Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserModelType Type { get; set; }
    public string Password { get; set; }
    public string? Description { get; set; }
    public uint? ExpectedValue { get; set; }
    public string? Role { get; set; }
    
    public string? ImageId { get; set; }
    public ImageModel? Image { get; set; }
    
    public IEnumerable<WorkModel>? Works { get; set; }
    public IEnumerable<UserSkillModel>? Skills { get; set; }
}