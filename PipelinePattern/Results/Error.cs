#nullable disable
using PipelinePattern.Results;

namespace PipelinePattern.Results;

public record Error(string Code, string Message, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorType.Failure);
    public static implicit operator string(Error error)
        => error?.Code ?? string.Empty;
    public static implicit operator Result(Error error)
        => Result.Failure(error);
    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Problem(string code, string description) =>
        new(code, description, ErrorType.Problem);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
    public static Error Create(Exception exception)
        => new Error("Unhandled.Exception", exception.Message, ErrorType.Problem);
}
