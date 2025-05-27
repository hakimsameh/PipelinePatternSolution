using Microsoft.Extensions.Logging;
using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class ValidateCurrencyStep(ILogger<ValidateCurrencyStep> logger) : IPipelineStep<SupplierPaymentContext>
{
    public int Order => 200;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Validating currency code: {CurrencyCode}, Step Order: {Order}", context.Request.Currency, Order);
        if (string.IsNullOrWhiteSpace(context.Request.Currency))
            return Task.FromResult(Result.Failure(Error.Validation("Currency", "Currency code is required")));
        logger.LogInformation("Currency code {CurrencyCode} is valid", context.Request.Currency);
        return next();
    }
}
