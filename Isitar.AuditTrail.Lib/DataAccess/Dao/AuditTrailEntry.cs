namespace Isitar.AuditTrail.Lib.DataAccess.Dao
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NodaTime;

    public class AuditTrailEntry<T> where T : Entity
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }
        public T Subject { get; set; }

        public AuditTrailEntryType EntryType { get; set; }

        public string FromValue { get; set; }
        public string ToValue { get; set; }

        public Instant Timestamp { get; set; }
    }

    public class AuditTrailEntryConfiguration<T> : IEntityTypeConfiguration<AuditTrailEntry<T>> where T : Entity
    {
        public void Configure(EntityTypeBuilder<AuditTrailEntry<T>> builder)
        {
            // builder.HasOne(auditTrialEntry => auditTrialEntry.Subject)
            //     .WithMany(subject => subject.AuditTrailEntries)
            //     .HasForeignKey(x => x.SubjectId);
        }
    }
}