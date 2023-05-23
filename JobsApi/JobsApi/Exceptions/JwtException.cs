namespace JobsApi.Exceptions;

public class JwtException : Exception
{
    public JwtException(string message) : base(message)
    {
    }
}