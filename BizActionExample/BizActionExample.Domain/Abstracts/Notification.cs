using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BizActionExample.Domain.Abstracts
{
    public struct Notification : IEquatable<Notification>
    {
        private List<ValidationResult> validationResults;
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool HasErrorCode => !string.IsNullOrEmpty(ErrorCode);

        public static Notification Instance()
        {
            return new Notification();
        }

        public void AddError(string fieldName, string errorMessage)
        {
            validationResults ??= new List<ValidationResult>();

            validationResults.Add(new ValidationResult(errorMessage, new[] { fieldName }));
        }

        public void AddErrors(ValidationResult[] errors)
        {
            validationResults ??= new List<ValidationResult>();
            validationResults.AddRange(errors);
        }

        public void AddErrorCode(string errorCode, string messageTemplate, params object[] parameters)
        {
            if (ErrorCode != null && !string.Equals(ErrorCode, errorCode, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"ErrorCode {ErrorCode} has been set. Only one errorCode support");
            }

            ErrorCode = errorCode;
            ErrorMessage = string.Format(CultureInfo.InvariantCulture, messageTemplate, parameters);
        }

        public IReadOnlyList<ValidationResult> Errors => validationResults ??
                                                         ArraySegment<ValidationResult>.Empty as
                                                             IReadOnlyList<ValidationResult>;

        public bool HasError => (validationResults != null && validationResults.Count > 0) ||
                                !string.IsNullOrEmpty(ErrorCode);

        public Notification Join(Notification other)
        {
            if (!string.IsNullOrEmpty(ErrorCode) && other.ErrorCode != ErrorCode)
            {
                throw new InvalidOperationException("Cannot join Error with difference ErrorCode.");
            }

            validationResults ??= new List<ValidationResult>();

            ErrorCode = other.ErrorCode;
            ErrorMessage = other.ErrorMessage;
            if (other.validationResults?.Any() == true)
            {
                validationResults.AddRange(other.validationResults);
            }

            return this;
        }

        public Notification Add(Dictionary<string, string[]> other)
        {
            foreach (var item in other)
            {
                validationResults.Add(new ValidationResult(item.Key, item.Value));
            }

            return this;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(ErrorCode))
            {
                return $"{ErrorCode}: {ErrorMessage}";
            }

            foreach (var item in validationResults)
            {
                builder.Append(item.ErrorMessage);
            }

            return builder.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (this == null && obj == null) return true;
            if (this == null && obj != null) return false;
            if (this != null && obj == null) return false;

            return obj is Notification other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(31, base.GetHashCode());
        }

        public static bool operator ==(Notification left, Notification right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Notification left, Notification right)
        {
            return !(left == right);
        }

        public bool Equals(Notification other)
        {
            return validationResults.Equals(other.validationResults) && ErrorCode == other.ErrorCode &&
                   ErrorMessage == other.ErrorMessage;
        }
    }
}