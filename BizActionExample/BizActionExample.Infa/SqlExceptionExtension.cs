using System;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BizActionExample.Infa
{
    public static class SqlExceptionExtension
    {
        /// <summary>
        /// Use below query to get sql error codes
        /// SELECT message_id AS Error, severity AS Severity,
        /// [Event Logged] = CASE is_event_logged WHEN 0 THEN 'No' ELSE 'Yes' END,
        /// [text]
        /// FROM sys.messages
        /// WHERE language_id = 1033 and[text] like '%The duplicate%'
        /// ORDER BY message_id
        /// </summary>
        private static readonly ArrayList DuplicateCodes = new ArrayList { 2601, 2627 };

        private static Regex DuplicateRegex = new Regex($"'([a-zA-Z._+])+'");

        public static void ThrowIfValidationException(this Exception exception)
        {
            if (exception == null) return;

            if (exception is SqlException)
            {
                var sqlException = exception as SqlException;
                Debug.WriteLine($"Found SqlException, continue to parse message {sqlException.Number}");
                if (DuplicateCodes.IndexOf(sqlException.Number) >= 0)
                {
                    var fieldName = ExtractFieldName(DuplicateRegex, sqlException.Message);
                    throw new ArgumentException("duplicated", fieldName);
                }
            }

            Debug.WriteLine("Trace for InnerException");
            exception.InnerException?.ThrowIfValidationException();
        }

        private static string ExtractFieldName(Regex regex, string message)
        {
            try
            {
                Debug.WriteLine(message);
                var result = regex.Matches(message);
                // expected the unique key following format: IX_[Table]_[FieldName]
                if (result.Count == 0 || result[1]?.Value?.LastIndexOf('_') < 0)
                {
                    return string.Empty;
                }

                var field = result[1].Value.Trim('\'').Substring(result[1].Value.LastIndexOf('_'));
                return field;
            }
            catch (IndexOutOfRangeException ex)
            {
                Trace.TraceError(ex.Message);
                return string.Empty;
            }
        }
    }
}