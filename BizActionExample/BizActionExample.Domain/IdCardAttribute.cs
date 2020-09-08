using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BizActionExample.Domain
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IdCardAttribute : ValidationAttribute
    {
        //public override string FormatErrorMessage(string name)
        //{
        //    if (ErrorMessage == null && ErrorMessageResourceName == null)
        //        ErrorMessage = LibraryResource.InvalidIdCard;
        //    return String.Format(CultureInfo.CurrentCulture, ErrorMessageString);
        //}

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    if (value.SafeString().IsEmpty())
        //        return ValidationResult.Success;
        //    if (Regex.IsMatch(value.SafeString(), ValidatePattern.IdCardPattern))
        //        return ValidationResult.Success;
        //    return new ValidationResult(FormatErrorMessage(string.Empty));
        //}
    }
}