namespace JobsApi.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string type, uint id) : base($"{type} whit id {id} not found")
    {
    }
    
    public NotFoundException(string type, string id) : base($"{type} whit id {id} not found")
    {
    }
}