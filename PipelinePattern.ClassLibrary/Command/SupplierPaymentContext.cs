using PipelinePattern.Interfaces;

namespace PipelinePattern.ClassLibrary.Command;

public class SupplierPaymentContext(AddSupplierPaymentCommand request, CancellationToken cancellationToken)
        : PipelineContextBase<AddSupplierPaymentCommand>(request, cancellationToken);