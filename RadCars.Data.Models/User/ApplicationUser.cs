// ReSharper disable VirtualMemberCallInConstructor
namespace RadCars.Data.Models.User;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

using Entities;
using Common.Contracts;

using static RadCars.Common.EntityValidationConstants.ApplicationUser;

public class ApplicationUser : IdentityUser<Guid>, IAuditInfo, IDeletableEntity
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();
        this.Listings = new HashSet<Listing>();
        this.FavoriteListings = new HashSet<UserFavoriteListing>();

        this.Roles = new HashSet<IdentityUserRole<Guid>>();
        this.Claims = new HashSet<IdentityUserClaim<Guid>>();
        this.Logins = new HashSet<IdentityUserLogin<Guid>>();
    }

    [Required]
    [StringLength(FirstNameMaximumLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(LastNameMaximumLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [ProtectedPersonalData]
    [StringLength(UserNameMaxLength)]
    public override string UserName { get; set; } = null!;

    [ProtectedPersonalData]
    [DataType(DataType.PhoneNumber)]
    [StringLength(PhoneNumberMaximumLength)]
    public override string PhoneNumber { get; set; } = null!;

    [NotMapped]
    public string FullName => $"{this.FirstName} {this.LastName}";

    public virtual ICollection<Listing> Listings { get; set; }

    public virtual ICollection<UserFavoriteListing> FavoriteListings { get; set; }

    //Audit log
    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    //SoftDelete
    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }

    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
}