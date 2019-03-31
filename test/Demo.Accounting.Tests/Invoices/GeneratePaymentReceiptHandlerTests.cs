using System.Linq;
using Demo.Accounting.Application.Invoices.Commands;
using Demo.Accounting.Domain.Invoices.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Demo.Accounting.Tests.Invoices
{
    [TestFixture]
    public class GeneratePaymentReceiptHandlerTests
    {
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
            TestInitializer.Events.Clear();

        }
        [Test]
        public void should_Extract_Invoice_Detail()
        {
            var command = new GeneratePaymentReceipt("X020-01",new decimal(5777.88));

            var result = _mediator.Send(command).Result;

            Assert.True(result.IsSuccess);
            Assert.True(TestInitializer.Events.OfType<PaymentReceiptGenerated>().Any());
        }

        [Test]
        public void should_Fail_Extract_Invoice_Detail()
        {
            var command = new GeneratePaymentReceipt("X020-01",new decimal(-5777.88));

            var result = _mediator.Send(command).Result;

            Assert.True(result.IsFailure);
            Assert.False(TestInitializer.Events.OfType<PaymentReceiptGenerated>().Any());
        }
    }
}
