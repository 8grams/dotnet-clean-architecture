using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace WebApi.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; }

        public ValidationException(string errorMsg) : base(errorMsg)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures, string errorMsg) : this(errorMsg)
        {
            var propertyNames = failures.Select(e => e.PropertyName).Distinct();

            foreach(var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
