namespace JobsApi.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string type, uint id) : base($"{type} whit id {id} is not found")
    {
    }
}