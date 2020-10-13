using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GenericBizRunner
{
    /// <summary>
    /// This contains the error hanlding part of the GenericBizRunner
    /// </summary>
    public class StatusGenericHandler : IStatusGeneric
    {
        public StatusGenericHandler(string header = "")
        {
            Header = header;
        }

        internal const string DefaultSuccessMessage = "Success";
        private readonly List<ErrorGeneric> _errors = new List<ErrorGeneric>();
        private string _successMessage = DefaultSuccessMessage;

        /// <summary>
        /// The header provides a prefix to any errors you add. Useful if you want to have a general prefix to all your errors
        /// e.g. a header if "MyClass" would produce error messages such as "MyClass: This is my error message."
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// This holds the list of ValidationResult errors. If the collection is empty, then there were no errors
        /// </summary>
        public IImmutableList<ErrorGeneric> Errors => _errors.ToImmutableList();

        /// <summary>
        /// This is true if any errors have been reistered
        /// </summary>
        public bool IsValid => !_errors.Any();

        /// <summary>
        /// On success this returns the message as set by the business logic, or the default messages set by the BizRunner
        /// If there are errors it contains the message "Failed with NN errors"
        /// </summary>
        public string Message
        {
            get => IsValid
                ? _successMessage
                : $"Failed with {_errors.Count} error" + (_errors.Count == 1 ? "" : "s");
            set => _successMessage = value;
        }

        /// <summary>
        /// This adds one error to the Errors collection
        /// </summary>
        /// <param name="errorMessage">The text of the error message</param>
        /// <param name="propertyNames">optional. A list of property names that this error applies to</param>
        public IStatusGeneric AddError(string errorMessage, params string[] propertyNames)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            _errors.Add(new ErrorGeneric(Header, new ValidationResult(errorMessage, propertyNames)));
            return this;
        }

        /// <summary>
        /// This adds one ValidationResult to the Errors collection
        /// </summary>
        /// <param name="validationResult"></param>
        public void AddValidationResult(ValidationResult validationResult)
        {
            _errors.Add(new ErrorGeneric(Header, validationResult));
        }

        /// <summary>
        /// This appends a collection of ValidationResults to the Errors collection
        /// </summary>
        /// <param name="validationResults"></param>
        public void AddValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            _errors.AddRange(validationResults.Select(x => new ErrorGeneric(Header, x)));
        }

        public IStatusGeneric CombineStatuses(IStatusGeneric status)
        {
            if (!status.IsValid)
            {
                _errors.AddRange(string.IsNullOrEmpty(Header)
                    ? status.Errors
                    : status.Errors.Select(x => new ErrorGeneric(Header, x)));
            }
            if (IsValid && status.Message != DefaultSuccessMessage)
                Message = status.Message;

            return this;
        }

        public string GetAllErrors(string seperator = "\n")
        {
            return _errors.Any()
                ? string.Join(seperator, Errors)
                : null;
        }

        public IStatusGeneric AddErrorCode(string errorCode, string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            _errors.Add(new ErrorGeneric(Header, new ValidationResult(errorMessage)));
            return this;
        }

        public bool HasErrors => _errors.Any();
    }
}