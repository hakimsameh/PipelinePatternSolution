using CQRS.Core.Interfaces;
using Microsoft.Extensions.Logging;
using PipelinePattern.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command;

internal sealed class AddSupplierPaymentCommandHandler(
    IPipelineExecutor<SupplierPaymentContext> executor,
    ILogger<AddSupplierPaymentCommandHandler> logger) 
    : IRequestHandler<AddSupplierPaymentCommand, Result>
{
    private readonly IPipelineExecutor<SupplierPaymentContext> _executor = executor;
    private readonly ILogger<AddSupplierPaymentCommandHandler> logger = logger;

    public async Task<Result> Handle(AddSupplierPaymentCommand request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling AddSupplierPaymentCommand for SupplierId: {SupplierId}", request.SupplierId);
        var context = new SupplierPaymentContext(request, cancellationToken);
        var result = await _executor.ExecuteAsync(context, cancellationToken);
        logger.LogInformation("AddSupplierPaymentCommand handled with result: {Result}", result);
        return result;
    }
}
