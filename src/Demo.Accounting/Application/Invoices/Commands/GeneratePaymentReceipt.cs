using CSharpFunctionalExtensions;
using MediatR;

namespace Demo.Accounting.Application.Invoices.Commands
{
    public class GeneratePaymentReceipt : IRequest<Result>
    {
        public string InvoiceNo { get; }
        public decimal Amount { get; }

        public GeneratePaymentReceipt(string invoiceNo, decimal amount)
        {
            InvoiceNo = invoiceNo;
            Amount = amount;
        }
    }
}
