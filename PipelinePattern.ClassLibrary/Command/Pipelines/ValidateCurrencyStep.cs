using Microsoft.Extensions.Logging;
using PipelinePattern.ClassLibrary.Command;
using PipelinePattern.Interfaces;
using PipelinePattern.Results;

namespace PipelinePattern.ClassLibrary.Command.Pipelines;

internal class ValidateCurrencyStep(ILogger<ValidateCurrencyStep> logger) : IPipelineStep<SupplierPaymentContext>
{
    public int Order => 200;

    public Task<Result> ProcessAsync(SupplierPaymentContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Validating currency code: {CurrencyCode}, Step Order: {Order}", context.CurrencyCode, Order);
        if (string.IsNullOrWhiteSpace(context.CurrencyCode))
            return Task.FromResult(Result.Failure(Error.Validation("Currency", "Currency code is required")));
        logger.LogInformation("Currency code {CurrencyCode} is valid", context.CurrencyCode);
        return next();
    }
}
