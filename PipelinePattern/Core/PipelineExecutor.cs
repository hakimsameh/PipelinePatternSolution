using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.Core;
internal class PipelineExecutor<TContext>(IEnumerable<IPipelineStep<TContext>> steps)
    : IPipelineExecutor<TContext>
    where TContext : IPipelineContextBase
{
    private readonly IList<IPipelineStep<TContext>> _steps = [.. steps.OrderBy(step => step.Order)];
    public Task<Result> ExecuteAsync(TContext context, CancellationToken cancellationToken = default)
    {
        Func<Task<Result>> next = () => Task.FromResult(Result.Success());
        foreach (var step in _steps.Reverse())
        {
            var current = step;
            var previousNext = next;
            next = async () =>
            {
                if (context.ContextResult.IsFailure)
                    return context.ContextResult;

                return await current.ProcessAsync(context, previousNext, cancellationToken);
            };
        }
        return next();
    }
}
