namespace PipelinePattern.Results;
public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None: // IsSuccess and Has Error
                throw new InvalidOperationException();
            case false when error == Error.None: // IsSuccess is False and No Error 
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, Error.None);
    public static Result<TValue> Create<TValue>(TValue? value, Error error)
        where TValue : class
        => value is null ? Failure<TValue>(error) : Success(value);
    public static Result<TValue> Create<TValue>(TValue value)
        where TValue : class
        => Success(value);

    public static Result Failure(Error error)
        => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error)
            => new(default!, false, error);
    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (var result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }
        return Success();
    }
}