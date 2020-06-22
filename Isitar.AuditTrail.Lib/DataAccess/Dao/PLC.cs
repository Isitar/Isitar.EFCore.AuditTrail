namespace Isitar.AuditTrail.Lib.DataAccess.Dao
{
    using System.Collections.Generic;

    public class PLC: Entity, IAuditTrailable<PLC>
    {
        public SwitchCabinet SwitchCabinet { get; set; }
        
        public string Name { get; set; }

        public PLC GetEntity()
        {
            return this;
        }

        public List<AuditTrailEntry<PLC>> AuditTrailEntries { get; set; }
    }
}