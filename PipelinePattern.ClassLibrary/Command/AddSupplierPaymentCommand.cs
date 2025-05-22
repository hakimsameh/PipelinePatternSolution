using CQRS.Core.Interfaces;
using SamSoft.Common.Results;

namespace PipelinePattern.ClassLibrary.Command;

public record AddSupplierPaymentCommand(Guid SupplierId,
    decimal Amount,
    string Currency,
    DateTime PaymentDate,
    string PaymentMethod,
    string ReferenceNumber
) : IRequest<Result>;
