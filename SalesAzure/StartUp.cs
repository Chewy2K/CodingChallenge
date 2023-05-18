using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SalesData.Models;
using SalesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(SalesAzure.StartUp))]
namespace SalesAzure
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string dbConnection;
            try
            {
                var contextOptions = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;
                var currentDirectory = contextOptions.AppDirectory;
                var config = new ConfigurationBuilder().SetBasePath(currentDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .Build();
                dbConnection = config["Values:SqlServerConnection"];
            }
            catch (Exception)
            {

                dbConnection = "";
            }

            var environmentValue = Environment.GetEnvironmentVariable("SqlServerConnection");
            var connectionString = (environmentValue != null) ?
                environmentValue : dbConnection;

            builder.Services.AddDbContext<SalesDBContext>(x =>
            {
                x.UseSqlServer(
                    connectionString, //dbConnection
                    options => options.EnableRetryOnFailure());
            });

            builder.Services.AddTransient<SaleService>();

            builder.Services.AddAutoMapper(typeof(StartUp));
        }
    }
}
