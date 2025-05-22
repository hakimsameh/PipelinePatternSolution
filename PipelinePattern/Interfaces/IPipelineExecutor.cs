using SamSoft.Common.Results;

namespace PipelinePattern.Interfaces;

public interface IPipelineExecutor<TContext>
    where TContext : IPipelineContext
{
    Task<Result> ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
}
