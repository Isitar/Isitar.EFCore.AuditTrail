namespace Isitar.AuditTrail.Lib.DataAccess
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AuditTrailExtensions;
    using Dao;
    using Microsoft.EntityFrameworkCore;

    public class AuditTrailContext : DbContext
    {
        // public override int SaveChanges()
        // { 
        //     ChangeTracker.DetectChanges();
        //     var relevantEntries = ChangeTracker.Entries()
        //         .Where(x => x.Entity.GetType().GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAuditTrailable<>)))
        //         .Where(x => new[] {EntityState.Added, EntityState.Modified, EntityState.Deleted}.Contains(x.State))
        //         .ToList();
        //     foreach (var relevantEntry in relevantEntries)
        //     {
        //         dynamic item;
        //         if (relevantEntry.Entity.GetType() == typeof(SwitchCabinet))
        //         {
        //             item = relevantEntry.Entity as IAuditTrailable<SwitchCabinet>;
        //         } else if (relevantEntry.Entity.GetType() == typeof(PLC))
        //         {
        //             item = relevantEntry.Entity as IAuditTrailable<PLC>;
        //         }
        //         else
        //         {
        //             continue;
        //         }
        //         AuditTrailableExtensions.AddAuditTrailentry(item, this);
        //     }
        //     return base.SaveChanges();
        // }

        public AuditTrailContext()
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "User ID=auditTrail;Password=auditTrail;Server=127.0.0.1;Port=5432;Database=auditTrail;Integrated Security=true;Pooling=true;";
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(connectionString, o => o.UseNodaTime());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(PLC))!);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SwitchCabinet> SwitchCabinets { get; set; }
        public DbSet<PLC> PLCs { get; set; }
    }
}