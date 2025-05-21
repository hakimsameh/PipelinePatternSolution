using PipelinePattern.Interfaces;

namespace PipelinePattern.ClassLibrary.Command;

public class SupplierPaymentContext : IPipelineContext
{
    public string? SupplierId { get; set; }
    public decimal Amount { get; set; }
    public string? ReferenceNo { get; set; }
    public string? CurrencyCode { get; set; }
    public DateTime DocumentDate { get; set; }
    public bool IsCashPayment { get; set; }
    public bool IsValid { get; set; } = true;
}
