using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Accounting.Domain.Invoices.Events;
using MediatR;

namespace Demo.Accounting.ConsoleApp
{
    public class ProcessInvoiceEventHandler:INotificationHandler<InvoiceGenerated>,INotificationHandler<PaymentReceiptGenerated>
    {
        public async Task Handle(InvoiceGenerated notification, CancellationToken cancellationToken)
        {
            var sendToUri = new Uri("rabbitmq://localhost/generate_invoice_queuex");
            var endPoint = await Program.BusControl.GetSendEndpoint(sendToUri);

            await endPoint.Send(notification);
        }

        public async Task Handle(PaymentReceiptGenerated notification, CancellationToken cancellationToken)
        {
            var sendToUri = new Uri("rabbitmq://localhost/generate_invoice_queuex");
            var endPoint = await Program.BusControl.GetSendEndpoint(sendToUri);

            await endPoint.Send(notification);
        }
    }
}
