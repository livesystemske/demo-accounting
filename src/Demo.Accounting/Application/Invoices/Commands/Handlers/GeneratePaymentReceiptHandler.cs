using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CSharpFunctionalExtensions;
using Demo.Accounting.Domain.Invoices;
using Demo.Accounting.Domain.Invoices.Events;
using MediatR;
using Serilog;

namespace Demo.Accounting.Application.Invoices.Commands.Handlers
{
    public class GeneratePaymentReceiptHandler:IRequestHandler<GeneratePaymentReceipt,Result>
    {
        private readonly IMediator _mediator;

        public GeneratePaymentReceiptHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result> Handle(GeneratePaymentReceipt request, CancellationToken cancellationToken)
        {
            Log.Information("Generating payment receipt...");
            try
            {
                if(request.Amount<=0)
                    throw new ArgumentException("Invalid payment amount !");
                var payment = new Payment(request.Amount, request.InvoiceNo);

                await _mediator.Publish(new PaymentReceiptGenerated(payment.Id));

                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "Generate Payment error!");
                return Result.Fail(e.Message);
            }
        }
    }
}
