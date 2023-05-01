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
}