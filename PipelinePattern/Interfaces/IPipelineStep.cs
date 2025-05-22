using SamSoft.Common.Results;

namespace PipelinePattern.Interfaces;

public interface IPipelineStep<TContext>
    where TContext : IPipelineContextBase
{
    int Order { get; }
    Task<Result> ProcessAsync(TContext context, Func<Task<Result>> next, CancellationToken cancellationToken);
}
