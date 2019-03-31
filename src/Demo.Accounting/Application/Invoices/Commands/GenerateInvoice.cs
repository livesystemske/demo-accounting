using CSharpFunctionalExtensions;
using MediatR;

namespace Demo.Accounting.Application.Invoices.Commands
{
    public class GenerateInvoice:IRequest<Result>
    {
        public string InvoiceNo { get;  }
        public string InvoiceLineDetail { get;  }

        public GenerateInvoice(string invoiceNo, string invoiceLineDetail)
        {
            InvoiceNo = invoiceNo;
            InvoiceLineDetail = invoiceLineDetail;
        }
    }
}
