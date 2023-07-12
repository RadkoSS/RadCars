namespace RadCars.Data.Common.Models;

using System;
using System.ComponentModel.DataAnnotations;

using Contracts;

public abstract class BaseModel<TKey> : IAuditInfo
{
    protected BaseModel() 
    { 
        this.CreatedOn = DateTime.UtcNow;
    }

    [Key]
    public TKey Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}