namespace Isitar.AuditTrail.Lib.AuditTrailExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using DataAccess.Dao;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using NodaTime;

    public static class AuditTrailableExtensions
    {
        public static void AddAuditTrailEntry<T>(this IAuditTrailable<T> auditTrailable, AuditTrailContext dbContext) where T : Entity
        {
            var entityEntry = dbContext.Entry(auditTrailable);
            AuditTrailEntry<T> auditTrailEntry;
            if (EntityState.Added == entityEntry.State)
            {
                auditTrailEntry = new AuditTrailEntry<T>
                {
                    Subject = auditTrailable.GetEntity(),
                    Timestamp = SystemClock.Instance.GetCurrentInstant(),
                    EntryType = AuditTrailEntryType.Insert,
                    FromValue = "",
                    ToValue = JsonConvert.SerializeObject(entityEntry.Entity, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore}),
                };
                auditTrailable.AuditTrailEntries.Add(auditTrailEntry);
                return;
            }
            
            var changedProperties = entityEntry.Properties.Where(p => p.IsModified).ToList();
            if (changedProperties.Count == 0)
            {
                return;
            }
            var (from, to) = changedProperties.Aggregate((From: "", To: ""), (carry, p) =>
            {
                if (!p.IsModified || p.CurrentValue.GetType() == typeof(ICollection<AuditTrailEntry<T>>))
                {
                    return carry;
                }

                return ($"{carry.From}{Environment.NewLine}{p.Metadata.Name}: {p.OriginalValue}", 
                    $"{carry.To}{Environment.NewLine}{p.Metadata.Name}: {p.CurrentValue}");
            });


            auditTrailable.AuditTrailEntries ??= new List<AuditTrailEntry<T>>();
            auditTrailEntry = new AuditTrailEntry<T>
            {
                Subject = auditTrailable.GetEntity(),
                Timestamp = SystemClock.Instance.GetCurrentInstant(),
                EntryType = entityEntry.State switch
                {
                    EntityState.Added => AuditTrailEntryType.Insert,
                    EntityState.Modified => AuditTrailEntryType.Update,
                    EntityState.Deleted => AuditTrailEntryType.Delete,
                    _ => AuditTrailEntryType.Update,
                },
                FromValue = from,
                ToValue = to,
            };
            auditTrailable.AuditTrailEntries.Add(auditTrailEntry);
        }
    }
}