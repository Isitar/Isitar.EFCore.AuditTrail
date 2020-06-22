namespace Isitar.AuditTrail.Lib.DataAccess.Dao
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}