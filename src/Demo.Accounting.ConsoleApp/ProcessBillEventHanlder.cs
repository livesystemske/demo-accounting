using System.Threading;
using System.Threading.Tasks;
using Demo.Accounting.Domain.Invoices.Events;
using MediatR;

namespace Demo.Accounting.ConsoleApp
{
    public class ProcessInvoiceEventHandler:INotificationHandler<InvoiceGenerated>,INotificationHandler<PaymentReceiptGenerated>
    {
        public Task Handle(InvoiceGenerated notification, CancellationToken cancellationToken)
        {
            Program.BusControl.Publish<InvoiceGenerated>(notification);
            return Task.CompletedTask;
        }

        public Task Handle(PaymentReceiptGenerated notification, CancellationToken cancellationToken)
        {
            Program.BusControl.Publish<PaymentReceiptGenerated>(notification);
            return Task.CompletedTask;
        }
    }
}
