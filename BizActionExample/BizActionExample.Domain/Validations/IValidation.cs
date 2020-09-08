using System;
using System.Collections.Generic;
using System.Text;

namespace BizActionExample.Domain.Validations
{
    public interface IValidation
    {
        ValidationResultCollection Validate();
    }
}
