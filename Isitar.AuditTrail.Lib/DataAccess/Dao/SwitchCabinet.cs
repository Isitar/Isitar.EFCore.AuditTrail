namespace Isitar.AuditTrail.Lib.DataAccess.Dao
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SwitchCabinet : Entity, IAuditTrailable<SwitchCabinet>
    {
        private ICollection<AuditTrailEntry<Entity>> auditTrailEntries;
        public string Location { get; set; }

        public SwitchCabinet GetEntity()
        {
            return this;
        }

        public List<AuditTrailEntry<SwitchCabinet>> AuditTrailEntries { get; set; }
        public List<PLC> PLCs { get; set; }
        
    }

    public class SwitchCabinetEntityTypeConfiguration : IEntityTypeConfiguration<SwitchCabinet>
    {
        public void Configure(EntityTypeBuilder<SwitchCabinet> builder)
        {
            builder.HasMany(switchCabinet => switchCabinet.PLCs)
                .WithOne(plc => plc.SwitchCabinet)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}