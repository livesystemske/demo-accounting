using System;

namespace Demo.Accounting.Domain.Invoices
{
    public class Payment
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNumber { get; set; }

        public Payment(decimal amount, string invoiceNumber)
        {
            Id = DateTime.Now.Ticks;
            Date=DateTime.Now;
            Amount = amount;
            InvoiceNumber = invoiceNumber;
        }


        public override string ToString()
        {
            return $"{Date:yyyy MMMM dd} {Amount:C}";
        }
    }
}
