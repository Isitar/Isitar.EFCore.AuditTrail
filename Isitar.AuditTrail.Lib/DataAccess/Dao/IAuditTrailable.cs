namespace Isitar.AuditTrail.Lib.DataAccess.Dao
{
    using System.Collections.Generic;
    
    public interface IAuditTrailable<T>  where T : Entity
    {
        public new T GetEntity();
        public new List<AuditTrailEntry<T>> AuditTrailEntries { get; set; }
    }
}