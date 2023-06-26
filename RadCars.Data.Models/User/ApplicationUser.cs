namespace RadCars.Data.Models.User;

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

using Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();
        this.Listings = new HashSet<Listing>();
    }

    public virtual ICollection<Listing> Listings { get; set; }
}