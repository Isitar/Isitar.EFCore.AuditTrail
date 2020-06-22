namespace Isitar.AuditTrail.CLI
{
    using System;
    using System.Threading.Tasks;
    using Lib.DataAccess;
    using Lib.DataAccess.Dao;
    using Lib.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<ISwitchCabinetService, SwitchCabinetService>()
                .AddDbContext<AuditTrailContext>()
                .AddSingleton<App>()
                .BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }
    }
}