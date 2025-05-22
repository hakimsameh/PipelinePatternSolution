using Microsoft.Extensions.Logging;
using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class FinalizePaymentStep(ILogger<FinalizePaymentStep> logger) : IPipelineStep<SupplierPaymentContext>
{
    public int Order => 400;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Finalizing payment for supplier ID: {SupplierId}, Step Order: {Order}", context.SupplierId, Order);
        context.IsValid = true;
        logger.LogInformation("Payment for supplier ID {SupplierId} finalized successfully", context.SupplierId);
        return next();
    }
}
