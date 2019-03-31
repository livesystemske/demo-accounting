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
    public class GenerateInvoiceHandler : IRequestHandler<GenerateInvoice, Result>
    {
        private readonly IMediator _mediator;

        public GenerateInvoiceHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result> Handle(GenerateInvoice request, CancellationToken cancellationToken)
        {
            Log.Information("Generating Invoice...");
            try
            {

                //Amount:2000|Period:2019Jan01|CallCharge:1900|Tax:100

                var invoiceDetails = request.InvoiceLineDetail.Split('|').ToList();

                DateTime.TryParse(GetAndReplace("Period",invoiceDetails), out var period);
                decimal.TryParse(GetAndReplace("CallCharge",invoiceDetails), out var callCharge);
                decimal.TryParse(GetAndReplace("Tax",invoiceDetails), out var tax);
                decimal.TryParse(GetAndReplace("Amount",invoiceDetails), out var amountDue);

                //save invoice
                var invoice = new Invoice(request.InvoiceNo, period, callCharge, tax, amountDue);

                await _mediator.Publish(new InvoiceGenerated(invoice.Number),cancellationToken);

                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "Generating Invoice error!");
                return Result.Fail(e.Message);
            }
        }

        private string GetAndReplace(string find, List<string> details)
        {
            var result= details
                .FirstOrDefault(x => x.ToLower().Contains(find.ToLower()))?.ToLower()
                .Replace(find.ToLower(),"")
                .Replace(":","")
                .Trim();

            if (null==result)
                throw new ArgumentException($"{find} Not found!");

            return result;
        }
    }
}
