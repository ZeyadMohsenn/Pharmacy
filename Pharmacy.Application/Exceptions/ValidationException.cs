using FluentValidation.Results;

namespace Pharmacy.Application.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures have occurred.")
    {
        Errors = failures.Select(failure => failure.ErrorMessage).ToList();
    }
}
