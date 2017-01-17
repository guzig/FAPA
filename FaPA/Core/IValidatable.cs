namespace FaPA.Core
{
    public interface IValidatable
    {
        DomainResult Validate();
        DomainResult ValidatePropertyValue(string prop);
        DomainResult DomainResult { get; }
        void HandleValidationResults();
        void HandleValidationResults( string propName );
    }

   
}