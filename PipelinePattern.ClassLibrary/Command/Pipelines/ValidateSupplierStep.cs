using Microsoft.Extensions.Logging;
using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class ValidateSupplierStep(ILogger<ValidateSupplierStep> logger) : IPipelineStep<SupplierPaymentContext>
{

    public int Order => 100;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        if (context.ContextResult.IsFailure) return Task.FromResult(context.ContextResult);
        logger.LogInformation("Validating supplier ID: {SupplierId}, Step Order: {Order}", context.Request.SupplierId, Order);
        if (context.Request.SupplierId == Guid.Empty)
            context.ContextResult = Result.Failure(Error.Validation("Supplier", "Supplier ID is required"));
        else
            logger.LogInformation("Supplier ID {SupplierId} is valid", context.Request.SupplierId);

        return next();
    }
}
