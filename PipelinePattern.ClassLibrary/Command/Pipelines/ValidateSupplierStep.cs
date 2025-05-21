using Microsoft.Extensions.Logging;
using PipelinePattern.ClassLibrary.Command;
using PipelinePattern.Interfaces;
using PipelinePattern.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class ValidateSupplierStep(ILogger<ValidateSupplierStep> logger) : IPipelineStep<SupplierPaymentContext>
{

    public int Order => 100;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Validating supplier ID: {SupplierId}, Step Order: {Order}", context.SupplierId, Order);
        if (string.IsNullOrWhiteSpace(context.SupplierId))
            return Task.FromResult(Result.Failure(Error.Validation("Supplier","Supplier ID is required")));
        logger.LogInformation("Supplier ID {SupplierId} is valid", context.SupplierId);
        return next();
    }
}
