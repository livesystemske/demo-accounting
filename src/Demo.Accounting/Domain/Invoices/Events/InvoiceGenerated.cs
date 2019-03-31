using MediatR;

namespace Demo.Accounting.Domain.Invoices.Events
{
    public class InvoiceGenerated:INotification
    {
        public string InvoiceNo { get;  }
        public InvoiceGenerated(string invoiceNo)
        {
            InvoiceNo = invoiceNo;
        }
    }
}
