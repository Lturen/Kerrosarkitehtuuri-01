namespace LeagueHub.Application.Exceptions;

// "Ei löydy" ei ole liiketoimintainvariantti — siksi Applicationissa, ei Domainissa.
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
