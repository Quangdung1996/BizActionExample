using System.ComponentModel.DataAnnotations;

namespace BizActionExample.Domain.Validations
{
    public interface IValidationRule
    {
        ValidationResult Validate();
    }
}