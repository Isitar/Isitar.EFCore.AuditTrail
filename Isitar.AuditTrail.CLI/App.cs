namespace Isitar.AuditTrail.CLI
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Lib.DataAccess;
    using Lib.DataAccess.Dao;
    using Lib.Services;
    using Microsoft.EntityFrameworkCore;

    public class App
    {
        private readonly AuditTrailContext dbContext;
        private readonly ISwitchCabinetService switchCabinetService;

        public App(AuditTrailContext dbContext, ISwitchCabinetService switchCabinetService)
        {
            this.dbContext = dbContext;
            this.switchCabinetService = switchCabinetService;
        }

        public void Run()
        {
            dbContext.Database.Migrate();
            
            var geb1Og = Guid.NewGuid();
            switchCabinetService.AddSwitchCabinet(new SwitchCabinet
            {
                Id = geb1Og,
                Location = "Geb. 1, Obergeschoss"
            });
            var geb1Ug = Guid.NewGuid();
            switchCabinetService.AddSwitchCabinet(new SwitchCabinet
            {
                Id = geb1Ug,
                Location = "Geb. 1, Untergeschoss"
            });
            var geb2Og = Guid.NewGuid();
            switchCabinetService.AddSwitchCabinet(new SwitchCabinet
            {
                Id = geb2Og,
                Location = "Geb. 2, Obergeschoss"
            });

            switchCabinetService.UpdateSwitchCabinet(geb1Og, new SwitchCabinet
            {
                Location = "Gebäude 1 Obergeschoss"
            });
            
            switchCabinetService.AddPLC(geb1Og, new PLC
            {
                Id = Guid.NewGuid(),
                Name = "PLC 1",
            });
            switchCabinetService.AddPLC(geb1Og, new PLC
            {
                Id = Guid.NewGuid(),
                Name = "PLC 2",
            });
            
            var og1AuditTrails = (switchCabinetService.FindById(geb1Og)).AuditTrailEntries.OrderBy(ae => ae.Timestamp);
            foreach (var auditTrailEntry in og1AuditTrails)
            {
                Console.WriteLine($"[{auditTrailEntry.Timestamp.ToString()}] [{auditTrailEntry.EntryType}] From: {auditTrailEntry.FromValue} {Environment.NewLine}To: {auditTrailEntry.ToValue}");
            }
        }
    }
}