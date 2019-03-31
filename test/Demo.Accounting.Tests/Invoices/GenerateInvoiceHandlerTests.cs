using System.Linq;
using Demo.Accounting.Application.Invoices.Commands;
using Demo.Accounting.Domain.Invoices.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Demo.Accounting.Tests.Invoices
{
    [TestFixture]
    public class GenerateInvoiceHandlerTests
    {
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
            TestInitializer.Events.Clear();

        }
        [Test]
        public void should_Generate_Invoice()
        {
            var command = new GenerateInvoice("INV001","Amount:2000|Period:2019Jan01|CallCharge:1900|Tax:100");

            var result = _mediator.Send(command).Result;

            Assert.True(result.IsSuccess);
            Assert.True(TestInitializer.Events.OfType<InvoiceGenerated>().Any());
        }

        [Test]
        public void should_Fail_Generate_Invoice()
        {
            var command = new GenerateInvoice("INV001","AmmountDue:2000|Period:2019Jan01|CallCharge:1900|Tax:100");

            var result = _mediator.Send(command).Result;

            Assert.True(result.IsFailure);
            Assert.False(TestInitializer.Events.OfType<InvoiceGenerated>().Any());
        }


    }
}
