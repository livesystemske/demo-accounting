using System.Collections.Generic;
using MediatR;

namespace Demo.Accounting.Domain.Invoices.Events
{
    public class PaymentReceiptGenerated:INotification
    {
        public long Id { get;  }

        public PaymentReceiptGenerated(long id)
        {
            Id = id;
        }
    }
}
