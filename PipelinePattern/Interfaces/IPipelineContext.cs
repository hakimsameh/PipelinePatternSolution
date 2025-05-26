using SamSoft.Common.Results;

namespace PipelinePattern.Interfaces;

public interface IPipelineContext<TRequest> : IPipelineContextBase
{
    TRequest Request { get; }
    CancellationToken CancellationToken { get; }
    Result ContextResult { get; }
    void SetContextResult(Error result);
}
