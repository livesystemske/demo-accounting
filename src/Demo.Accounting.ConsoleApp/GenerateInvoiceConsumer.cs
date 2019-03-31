using System.Threading.Tasks;
using Demo.Accounting.Application.Invoices.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Demo.Accounting.ConsoleApp
{
    public class GenerateInvoiceConsumer : IConsumer<BillInvoiceExtracted>
    {
        private readonly IMediator _mediator;

        public GenerateInvoiceConsumer()
        {
            _mediator = Program.ServiceProvider.GetService<IMediator>();
        }

        public async Task Consume(ConsumeContext<BillInvoiceExtracted> context)
        {
            Log.Information($"Processing {context.Message.InvoiceNo}");

            await _mediator.Send(new GenerateInvoice(context.Message.InvoiceNo, context.Message.InvoiceLineDetail));
        }
    }
    public class BillInvoiceExtracted
    {
        public string InvoiceNo { get; set; }
        public string InvoiceLineDetail { get; set; }
    }
}
