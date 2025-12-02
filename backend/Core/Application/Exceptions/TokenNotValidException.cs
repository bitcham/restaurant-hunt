namespace Core.Application.Exceptions;

public class TokenNotValidException : Exception
{
    public TokenNotValidException() : base("The token has expired.")
    {
    }
    
    public TokenNotValidException(string message) : base(message)
    {
    }
    
    public TokenNotValidException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
}