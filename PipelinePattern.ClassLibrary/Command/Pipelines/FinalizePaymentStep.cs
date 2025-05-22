using Microsoft.Extensions.Logging;
using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class FinalizePaymentStep(ILogger<FinalizePaymentStep> logger) 
    : IPipelineStep<SupplierPaymentContext>
{
    public int Order => 400;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        if (context.ContextResult.IsFailure) return Task.FromResult(context.ContextResult);
        logger.LogInformation("Finalizing payment for supplier ID: {SupplierId}, Step Order: {Order}", context.Request.SupplierId, Order);
        context.ContextResult = Result.Success();
        logger.LogInformation("Payment for supplier ID {SupplierId} finalized successfully", context.Request.SupplierId);
        return next();
    }
}
