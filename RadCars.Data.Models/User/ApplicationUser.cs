namespace RadCars.Data.Models.User;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();
        this.Listings = new HashSet<Listing>();
        this.Favorites = new HashSet<UserFavoriteListing>();
    }

    //[Required]
    //public string Country { get; set; } = null!;

    //[Required]
    //public string FirstName { get; set; } = null!;

    //[Required]
    //public string LastName { get; set; } = null!;

    //public string FullName => $"{this.FirstName} {this.LastName}";

    public virtual ICollection<Listing> Listings { get; set; }

    public virtual ICollection<UserFavoriteListing> Favorites { get; set; }
}