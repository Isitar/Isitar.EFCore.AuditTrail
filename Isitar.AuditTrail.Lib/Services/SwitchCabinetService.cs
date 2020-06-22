namespace Isitar.AuditTrail.Lib.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuditTrailExtensions;
    using DataAccess;
    using DataAccess.Dao;
    using Microsoft.EntityFrameworkCore;

    public class SwitchCabinetService : ISwitchCabinetService
    {
        private readonly AuditTrailContext dbContext;

        public SwitchCabinetService(AuditTrailContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddSwitchCabinet(SwitchCabinet switchCabinet)
        {
            var sc = new SwitchCabinet
            {
                Id = switchCabinet.Id,
                Location = switchCabinet.Location,
                AuditTrailEntries = new List<AuditTrailEntry<SwitchCabinet>>()
            };
            dbContext.SwitchCabinets.Add(sc);
            sc.AddAuditTrailEntry(dbContext);
            dbContext.SaveChanges();
        }

        public void UpdateSwitchCabinet(Guid id, SwitchCabinet switchCabinet)
        {
            var origSc = dbContext.SwitchCabinets.FirstOrDefault(sc => sc.Id.Equals(id));
            if (null == origSc)
            {
                return;
            }

            origSc.Location = switchCabinet.Location;
            origSc.AddAuditTrailEntry(dbContext);
            dbContext.SaveChanges();
        }

        public void DeleteSwitchCabinet(Guid id)
        {
            var sc = dbContext.SwitchCabinets.FirstOrDefault(sc => sc.Id.Equals(id));
            if (null == sc)
            {
                return;
            }

            dbContext.Remove(sc);
        }

        public void AddPLC(Guid switchCabinetId, PLC plc)
        {
            var switchCabinet = dbContext.SwitchCabinets.Include(sc => sc.PLCs).First(sc => sc.Id.Equals(switchCabinetId));
            var newPlc = new PLC
            {
                // Id = plc.Id,
                Name = plc.Name,
            };
            switchCabinet.PLCs.Add(newPlc);

            switchCabinet.AddAuditTrailEntry(dbContext);
            dbContext.SaveChanges();
        }

        public SwitchCabinet FindById(Guid id)
        {
            return dbContext.SwitchCabinets
                .AsNoTracking()
                .Include(sc => sc.AuditTrailEntries)
                .FirstOrDefault(sc => sc.Id.Equals(id));
        }
    }
}