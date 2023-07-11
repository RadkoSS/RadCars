namespace RadCars.Data.Models.User;

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

using Entities;
using Common.Contracts;

public class ApplicationUser : IdentityUser<Guid>, IAuditInfo, IDeletableEntity
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();
        this.Listings = new HashSet<Listing>();
        this.Favorites = new HashSet<UserFavoriteListing>();
    }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    //[Required]
    //public string FirstName { get; set; } = null!;

    //[Required]
    //public string LastName { get; set; } = null!;

    //public string FullName => $"{this.FirstName} {this.LastName}";

    public virtual ICollection<Listing> Listings { get; set; }

    public virtual ICollection<UserFavoriteListing> Favorites { get; set; }
}