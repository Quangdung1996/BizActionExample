using System.Linq;

namespace BizActionExample.Domain.Validations
{
    public class ThrowHandler : IValidationHandler
    {
        public void Handle(ValidationResultCollection results)
        {
            if (results.IsValid)
                return;
            //throw new Warning(results.First().ErrorMessage);
        }
    }
}