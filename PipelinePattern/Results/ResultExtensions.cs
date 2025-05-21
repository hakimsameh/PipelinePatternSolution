﻿namespace PipelinePattern.Results;

public static class ResultExtensions
{
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
    {
        if (result.IsFailure)
        {
            return result;
        }

        return result.IsSuccess && predicate(result.Value) ? result : Result.Failure<T>(error);
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func) =>
        result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Error);

    public static async Task<Result<TOut>> Map<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> func)
    {
        var result = await resultTask;
        return result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Error);
    }

    public static async Task<bool> Map(this Task<Result> resultTask)
    {
        var result = await resultTask;
        return result.IsSuccess;
    }

    public static async Task<Result> Bind<TIn>
        (this Result<TIn> result, Func<TIn, Task<Result>> func)
        => result.IsSuccess ? await func(result.Value) : Result.Failure(result.Error);

    public static async Task<Result<TOut>> Bind<TIn, TOut>
        (this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func)
        => result.IsSuccess ? await func(result.Value) : Result.Failure<TOut>(result.Error);

    public static Result<TOut> TryCatch<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func, Error error)
    {
        try
        {
            return result.IsSuccess ? Result.Success(func(result.Value)) : Result.Failure<TOut>(result.Error);
        }
        catch
        {
            return Result.Failure<TOut>(error);
        }
    }

    public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }

        return result;
    }

    public static async Task<T> Match<T>
        (this Task<Result> resultTask, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        var result = await resultTask;
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }

    public static async Task<TOut> Match<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> onSuccess,
        Func<string, TOut> onFailure)
    {
        var result = await resultTask;
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error.Message);
    }

    public static Result FirstFailure(this IEnumerable<Result> results)
        => results.FirstOrDefault(result => result.IsFailure) ?? Result.Success();
}