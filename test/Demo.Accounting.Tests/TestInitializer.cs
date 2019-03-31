using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Demo.Accounting.Application.Invoices.Commands.Handlers;
using Demo.Accounting.Domain.Invoices.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Demo.Accounting.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static List<object> Events;

        [OneTimeSetUp]
        public void Init()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.NUnitOutput()
                .CreateLogger();

            var services = new ServiceCollection()
                .AddMediatR(typeof(GenerateInvoiceHandler).Assembly,
                    typeof(TestEventHandler).Assembly);

            ServiceProvider = services.BuildServiceProvider();
            Events = new List<object>();
            Log.Debug("Tests initialized");
        }

        private class TestEventHandler :
            INotificationHandler<InvoiceGenerated>,
            INotificationHandler<PaymentReceiptGenerated>
        {
            public Task Handle(InvoiceGenerated notification, CancellationToken cancellationToken)
            {
                TestInitializer.Events.Add(notification);
                Log.Information($"{notification.InvoiceNo}");
                return Task.CompletedTask;
            }

            public Task Handle(PaymentReceiptGenerated notification, CancellationToken cancellationToken)
            {
                TestInitializer.Events.Add(notification);
                Log.Information($"{notification.Id}");
                return Task.CompletedTask;
            }
        }
    }
}
