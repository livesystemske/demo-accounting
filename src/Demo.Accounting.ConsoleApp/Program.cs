using System;
using System.IO;
using Demo.Accounting.Application.Invoices.Commands;
using Demo.Accounting.Application.Invoices.Commands.Handlers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Demo.Accounting.ConsoleApp
{
    class Program
    {
        public static IServiceProvider ServiceProvider;
        public static IConfiguration Configuration;
        public static IBusControl BusControl;

        static void Main(string[] args)
        {
            Init();

            BusControl = ConfigureBus();
            BusControl.Start();

            Log.Information("app started !");

            Console.ReadLine();

            BusControl.Stop();
        }

        static void Init()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting app..");
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GenerateInvoiceHandler).Assembly,typeof(ProcessInvoiceEventHandler).Assembly);
            ServiceProvider = services.BuildServiceProvider();
        }

        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.UseSerilog();

                var host= cfg.Host(new Uri(Configuration["RabbitMQ:uri"]), h =>
                {
                    h.Username(Configuration["RabbitMQ:username"]);
                    h.Password(Configuration["RabbitMQ:password"]);
                });

                cfg.ReceiveEndpoint(host, "generate_invoice_queue", e =>
                {
                    e.Consumer<GenerateInvoiceConsumer>();
                });
            });
        }
    }
}
