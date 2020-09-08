using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BizActionExample.Domain.Validations
{
    public class ValidationResultCollection : IEnumerable<ValidationResult>
    {
        private readonly List<ValidationResult> _results;

        public ValidationResultCollection() : this("")
        {
        }

        public ValidationResultCollection(string result)
        {
            _results = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(result))
                return;
            _results.Add(new ValidationResult(result));
        }

        public static readonly ValidationResultCollection Success = new ValidationResultCollection();

        public bool IsValid => _results.Count == 0;

        public int Count => _results.Count;

        public void Add(ValidationResult result)
        {
            if (result == null)
                return;
            _results.Add(result);
        }

        public void AddList(IEnumerable<ValidationResult> results)
        {
            if (results == null)
                return;
            foreach (var result in results)
                Add(result);
        }

        IEnumerator<ValidationResult> IEnumerable<ValidationResult>.GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        public override string ToString()
        {
            if (IsValid)
                return string.Empty;
            return _results.First().ErrorMessage;
        }
    }
}