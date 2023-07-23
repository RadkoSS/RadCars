// ReSharper disable VirtualMemberCallInConstructor
namespace RadCars.Data.Models.User;

using System;

using Microsoft.AspNetCore.Identity;

using Common.Contracts;

public class ApplicationRole : IdentityRole<Guid>, IAuditInfo, IDeletableEntity
{
    public ApplicationRole()
        : this(string.Empty)
    {}

    public ApplicationRole(string name)
        : base(name)
    {
        this.Id = Guid.NewGuid();
    }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}