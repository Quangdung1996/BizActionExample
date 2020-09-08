using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizActionExample.Domain.Validations
{
    public static class DataAnnotationValidation
    {
        public static ValidationResultCollection Validate(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            var result = new ValidationResultCollection();
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(target, null, null);
            var isValid = Validator.TryValidateObject(target, context, validationResults, true);
            if (!isValid)
                result.AddList(validationResults);
            return result;
        }
    }
}