namespace BizActionExample.Domain.Validations
{
    public interface IValidationHandler
    {
        void Handle(ValidationResultCollection results);
    }
}