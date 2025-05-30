﻿using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.Core;

public abstract class PipelineContextBase<TRequest>
    (TRequest request, CancellationToken cancellationToken) : IPipelineContext<TRequest>
{
    public TRequest Request { get; } = request;

    public CancellationToken CancellationToken { get; } = cancellationToken;

    public Result ContextResult { get; private set; } = Result.Success();
    public void SetContextResult(Error result)
    {
        ContextResult = Result.Failure(result);
    }
}