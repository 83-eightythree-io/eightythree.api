namespace Application.Organizations.CreateOrganization.Exceptions;

public class TermsAndConditionsNotAcceptedException : Exception
{
    public TermsAndConditionsNotAcceptedException(string message) : base(message)
    {
        
    }
}