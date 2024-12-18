using FluentValidation.Results;

namespace ProjectPersonal.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failture have occurred")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failuresGroup => failuresGroup.Key, failuresGroup => failuresGroup.ToArray());
        }

        public Dictionary<string, string[]> Errors { get; }
    }
}
