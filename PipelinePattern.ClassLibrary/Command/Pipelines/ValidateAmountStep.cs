using Microsoft.Extensions.Logging;
using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class ValidateAmountStep(ILogger<ValidateAmountStep> logger) : IPipelineStep<SupplierPaymentContext>
{
    public int Order => 300;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Validating amount: {Amount}, Step Order: {Order}", context.Request.Amount, Order);
        if (context.Request.Amount <= 0)
            context.SetContextResult(Error.Validation("Amount", "Amount must be greater than zero"));//);
        logger.LogInformation("Amount {Amount} is valid", context.Request.Amount);
        return next();
    }
}
